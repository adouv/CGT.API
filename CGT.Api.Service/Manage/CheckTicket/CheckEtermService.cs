using System;
using System.Collections.Generic;
using System.Text;
using CGT.CheckTicket.Service;
using CGT.PetaPoco.Repositories.CgtTravel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.Event.Model.Manage;
using CGT.SuspendedService;
using CGT.Entity.CgtModel;
using CGT.Api.Service.Manage.Remoney;
using CGT.Entity.CgtTravelModel;
using CGT.DDD.MQ;
using Newtonsoft.Json;
using CGT.DDD.Logger;

namespace CGT.Api.Service.Manage.CheckTicket {
    /// <summary>
    /// 黑屏验证服务类
    /// </summary>
    public class CheckEtermService : CheckTicketBaseService {
        /// <summary>
        /// 批次详情集合
        /// </summary>
        public ManageRiskModel ManageRisk = new ManageRiskModel();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model"></param>
        public CheckEtermService(ManageRiskModel model) : base() {
            ManageRisk = model;
        }


        /// <summary>
        /// 验证
        /// </summary>
        public override void Validate() {
            base.Validate();
        }

        /// <summary>
        /// 执行调用黑屏0验证小何  1黑屏   2先小何后黑屏 
        /// </summary>
        public override void Execute() {

            switch (ManageRisk.baseRiskModelList[0].EtermType) {
                case -1://测试规则直接调白名单
                    new CheckEnterpriseWhiteListService(ManageRisk).Execute();//调用白名单验证
                    break;
                case 0: //验证小何接口
                    CheckXH();
                    break;
                case 1: //验证黑屏接口
                    CheckEterm();
                    break;
                case 2: //先小何后黑屏
                    CheckXH();
                    break;
                default:
                    //记录风控规则不正确
                    LoggerFactory.Instance.Logger_Debug("风控规则不正确:" + ManageRisk.baseRiskModelList[0].EtermType.ToString(), "CheckEtermService");
                    break;
            }

        }

        /// <summary>
        /// 验证小何
        /// </summary>
        public void CheckXH() {
            bool IsRegist = false;
            string TravelBatchId = ManageRisk.TravelBatchId;
            long EnterpriseID = ManageRisk.EnterpriseID;
            #region 组织注册小何参数

            List<PreRegistrationRequestView> RegistList = new List<PreRegistrationRequestView>();
            foreach (var item in ManageRisk.baseRiskModelList) {
                PreRegistrationRequestView Regist = new PreRegistrationRequestView() {
                    TicketNo = item.TicketNum.Trim(),
                    Name = item.PersonName.Trim()
                };
                RegistList.Add(Regist);
            }

            #endregion

            //最多注册三次
            for (int i = 0; i < 3; i++) {
                var Result = new PreRegistrationProcessor(RegistList).Execute();
                if (Result.Success) {
                    IsRegist = true;
                    //修改注册状态和小何批次号(根据批次号和企业编号)
                    ManageRisk.baseRiskModelList.ForEach(x => x.UUId = Result.Message);
                    ManageRisk.baseRiskModelList.ForEach(x => x.RegisterStatus = 1);
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, "", "", 0, 0, Result.Message, 1, 0, 0, "");
                    break;
                }
                else {
                    continue;
                }
            }

            #region 小何注册失败处理

            if (!IsRegist) {
                ManageRisk.baseRiskModelList.ForEach(x => x.FailReason = "小何注册失败（xh）|");
                new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, "", "", 0, 0, "", 0, 0, 0, "小何注册失败（xh）|");

                if (ManageRisk.baseRiskModelList[0].EtermType == 2)//小何+黑屏
                {
                    CheckEterm();
                }
                else {
                    //调用王帅核算比率返现接口
                    LoggerFactory.Instance.Logger_Debug("调用王帅核算比率返现,list集合：" + JsonConvert.SerializeObject(ManageRisk), "CheckEtermService");
                    try {
                        new RemoneyService(ManageRisk).Execute();
                    }
                    catch (Exception ex) {
                        LoggerFactory.Instance.Logger_Debug("返现报错：" + ex.Message, "CheckEtermService");
                    }
                }
            }

            #endregion

        }

        /// <summary>
        /// 黑屏接口验证
        /// </summary>
        public void CheckEterm() {
            string TravelBatchId = ManageRisk.TravelBatchId;
            long EnterpriseID = ManageRisk.EnterpriseID;
            int TravelRiskType = ManageRisk.baseRiskModelList[0].TravelRiskType;
            UserAccount userAccount = new UserAccountRep().GetUserAccount(new UserAccount() { PayCenterCode = ManageRisk.PayCenterCode });
            var interfaceAccount = new InterfaceAccountRep().GetInterfaceAccount(new InterfaceAccount() { MerchantCode = userAccount.MerchantCode });
            var travelRisk = new TravelRiskRep().GetTravelRiskByEnterpriseID(new TravelRisk() { EnterpriseID = Convert.ToInt32(ManageRisk.baseRiskModelList[0].EnterpriseID), PayCenterCode = ManageRisk.PayCenterCode });
            if (!string.IsNullOrWhiteSpace(interfaceAccount.SuspendedServiceUrl)) {
                string strEtermSuccess = "";//黑屏接口调用成功的票号集合
                string strOrderPrice = "";  //票价验证失败票号集合
                string strOrderPersonName = "";//黑屏规则验证成功票号集合
                string strNoPersonName = "";   //黑屏规则验证失败票号集合
                foreach (var item in ManageRisk.baseRiskModelList) {
                    var commandArgs = new CommandArgs() {
                        Airline = item.FlightNo,
                        Catelog = "Ticket",
                        Command = "Checkinfo",
                        TicketType = "BSP",
                        Args = new OperateArgs() {
                            TicketNo = item.TicketNum
                        }
                    };
                    var ticketProcessor = new TicketProcessor(commandArgs, interfaceAccount.SuspendedServiceUrl).Execute();
                    if (ticketProcessor.Success) {
                        strEtermSuccess += "'" + item.TicketNum + "',";
                        item.EtermStatus = 1;//修改实体黑屏接口调用状态为成功
                        if (item.EtermType != 0)//包含黑屏
                        {
                            //验证订单金额
                            if (Convert.ToDecimal(ticketProcessor.Result.ticketData.Price) * travelRisk.TicketMultiple != 0 && Convert.ToDecimal(ticketProcessor.Result.ticketData.Price) * travelRisk.TicketMultiple < item.OrderAmount)//刨除票价验证失败订单（需取表字段配置）
                            {
                                strOrderPrice += "'" + item.TicketNum + "',";
                            }
                            else if (item.TravelRiskType != 1) //验价成功 且 风控规则不是纯白名单验证的 验证姓名
                            {
                                //验证姓名
                                if (item.PersonName.Trim() == ticketProcessor.Result.ticketData.Name.Trim()) {
                                    strOrderPersonName += "'" + item.TicketNum + "',";
                                    item.BlackResultState = 1; //设置黑屏验证结果为成功
                                }
                                else {
                                    strNoPersonName += "'" + item.TicketNum + "',";
                                    item.FailReason = "姓名验证失败（hp）|";
                                }
                            }
                        }

                    }
                    else {
                        //验证包含黑屏接口
                        if (item.EtermType != 0) {
                            strOrderPrice += "'" + item.TicketNum + "',";//添加验票价失败订单
                            item.FailReason = ticketProcessor.Message + "（hp）|";
                        }
                    }

                }
                if (!string.IsNullOrWhiteSpace(strEtermSuccess))//黑屏接口调用成功的票号
                {
                    strEtermSuccess = strEtermSuccess.Substring(0, strEtermSuccess.Length - 1);
                    //修改黑屏接口调用状态为成功
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, strEtermSuccess, "", 0, 0, "", 0, 0, 1, "");
                    LoggerFactory.Instance.Logger_Debug("黑屏接口调用成功：" + strEtermSuccess, "CheckEtermService");
                }
                //票价验证失败的票号
                if (!string.IsNullOrWhiteSpace(strOrderPrice)) {
                    strOrderPrice = strOrderPrice.Substring(0, strOrderPrice.Length - 1);
                    ManageRisk.baseRiskModelList.RemoveAll(i => strOrderPrice.Contains(i.TicketNum));
                    //记录验证票价失败的订单
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, strOrderPrice, "", 0, 0, "", 0, 0, 0, "票价验证失败（hp）");
                    //记录验证票价失败的订单日志
                    LoggerFactory.Instance.Logger_Debug("验证票价失败：" + strOrderPrice, "CheckEtermService");

                    //清除缓存
                    new RemoneyNotifyService().ModifyTicketCache(new List<string>(strOrderPrice.Substring(1, strOrderPrice.Length - 2).Split("','")));
                }
                //黑屏姓名验证成功的票号
                if (!string.IsNullOrWhiteSpace(strOrderPersonName)) {
                    strOrderPersonName = strOrderPersonName.Substring(0, strOrderPersonName.Length - 1);
                    //修改黑屏验证成功状态
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, strOrderPersonName, "", 0, 1, "", 0, 0, 0, "");
                    //修改黑屏验证成功状态日志
                    LoggerFactory.Instance.Logger_Debug("黑屏姓名验证成功" + strOrderPersonName, "CheckEtermService");
                }
                //黑屏姓名验证失败的票号
                if (!string.IsNullOrWhiteSpace(strNoPersonName)) {
                    strNoPersonName = strNoPersonName.Substring(0, strNoPersonName.Length - 1);
                    //修改黑屏验证失败原因
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, strNoPersonName, "", 0, 1, "", 0, 0, 0, "姓名验证失败（hp）|");
                }
                if (TravelRiskType != 0 && ManageRisk.baseRiskModelList.Count > 0)//判断风控规则不是 纯黑屏 
                {
                    //调用白名单验证
                    new CheckEnterpriseWhiteListService(ManageRisk).Execute();
                }
                //调用王帅核算比率返现接口
                LoggerFactory.Instance.Logger_Debug("调用王帅核算比率返现,list集合：" + JsonConvert.SerializeObject(ManageRisk), "CheckEtermService");
                try {
                    new RemoneyService(ManageRisk).Execute();
                }
                catch (Exception ex) {
                    LoggerFactory.Instance.Logger_Debug("返现报错：" + ex.Message, "CheckEtermService");
                }
            }
            else //黑屏地址为空
            {
                //记录验证失败原因
                ManageRisk.baseRiskModelList.ForEach(i => i.FailReason = "黑屏验证失败（hp地址为空）|");
                new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, "", "", 0, 0, "", 0, 0, 0, "黑屏验证失败（hp地址为空）|");
                //调用王帅核算比率返现接口
                LoggerFactory.Instance.Logger_Debug("调用王帅核算比率返现,list集合：" + JsonConvert.SerializeObject(ManageRisk), "CheckEtermService");
                try {
                    new RemoneyService(ManageRisk).Execute();
                }
                catch (Exception ex) {
                    LoggerFactory.Instance.Logger_Debug("返现报错：" + ex.Message, "CheckEtermService");
                }
            }
        }
    }
}
