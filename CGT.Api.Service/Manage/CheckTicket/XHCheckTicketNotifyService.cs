using System;
using System.Collections.Generic;
using System.Text;
using CGT.CheckTicket.Service;
using Newtonsoft.Json;
using CGT.PetaPoco.Repositories.CgtLog;
using CGT.Event.Model.Manage;
using CGT.Api.Service.Manage.Remoney;
using CGT.PetaPoco.Repositories.CgtTravel;
using CGT.Entity.CgtTravelModel;
using CGT.DDD.Logger;

namespace CGT.Api.Service.Manage.CheckTicket
{
    /// <summary>
    /// 小何回调服务类
    /// </summary>
    public class XHCheckTicketNotifyService : CheckTicketBaseService
    {
        /// <summary>
        /// 小何批次号
        /// </summary>
        public string uuid { get; set; }

        /// <summary>
        /// 小何返回的数据json
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// 导入信息集合
        /// </summary>
        ManageRiskModel ManageRisk = new ManageRiskModel();

        

        public XHCheckTicketNotifyService(string UUID, string DATA) : base()
        {
            uuid = UUID;
            data = DATA;
        }

        /// <summary>
        /// 验证
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrWhiteSpace(uuid))
            {
                //记录日志：uuid不能为空
                LoggerFactory.Instance.Logger_Debug("uuid不能为空", "XHCheckTicketNotifyService");
                return;
            }
            if (string.IsNullOrWhiteSpace(data))
            {
                //记录日志：data不能为空
                LoggerFactory.Instance.Logger_Debug("data不能为空", "XHCheckTicketNotifyService");
                return;
            }

        }
        /// <summary>
        /// 执行 
        /// </summary>
        public override void Execute()
        {
            Validate();
            List<BaseRiskModel> list = new List<BaseRiskModel>();
            //根据小何批次号获取本地票号信息
            List<TravelBatchOrder> TravelBatchOrderList = new TravelBatchOrderRep().getManageRiskModelByUUId(uuid);
            foreach (var item in TravelBatchOrderList)
            {
                BaseRiskModel rm = new BaseRiskModel()
                {
                    BlackResultState = Convert.ToInt32(item.BlackResultState),
                    CheckStatus = Convert.ToInt32(item.CheckStatus),
                    DepartureTime = Convert.ToDateTime(item.DepartureTime),
                    EnterpriseID = Convert.ToInt64(item.EnterpriseId),
                    EnterpriseName = item.EnterpriseName,
                    EtermStatus = Convert.ToInt32(item.EtermStatus),
                    EtermType = Convert.ToInt32(item.EtermType),
                    FlightNo = item.FlightNo,
                    //PayCenterCode = item.PayCenterCode,
                    PNR = item.PNR,
                    RegisterStatus = Convert.ToInt32(item.RegisterStatus),
                    TicketNum = item.TicketNo,
                    TicketTime = Convert.ToDateTime(item.TicketTime),
                    //TravelBatchId = item.TravelBatchId,
                    TravelRiskType = Convert.ToInt32(item.TravelRiskType),
                    UUId = item.UUId,
                    WhiteResultState = Convert.ToInt32(item.WhiteResultState),
                    PersonName = item.PassengerName,
                    ArriveCode = item.ArriveCode,
                    Cabin = item.Cabin,
                    DepartCode = item.DepartCode,
                    OrderAmount = Convert.ToDecimal(item.OrderAmount),
                    TicketPrice = Convert.ToDecimal(item.TicketPrice),
                    FailReason = item.MatchResult
                };
                list.Add(rm); 
            }
            ManageRisk.baseRiskModelList = list;
            TravelBatch TravelBatch = new TravelBatchRep().GetTravelBatch(TravelBatchOrderList[0].TravelBatchId, TravelBatchOrderList[0].EnterpriseId.ToString());
            ManageRisk.TravelBatchId = TravelBatchOrderList[0].TravelBatchId;
            ManageRisk.EnterpriseID= Convert.ToInt64(TravelBatchOrderList[0].EnterpriseId);
            ManageRisk.EnterpriseName = TravelBatchOrderList[0].EnterpriseName;
            ManageRisk.AccountPeriod =Convert.ToString(TravelBatch.AccountPeriod);
            ManageRisk.BackReapalNo = TravelBatch.BackReapalNo;
            ManageRisk.FactoringEmail = TravelBatch.FactoringEmail;
            ManageRisk.FactoringInterestRate = Convert.ToDecimal(TravelBatch.FactoringInterestRate);
            ManageRisk.FactoringName = TravelBatch.FactoringName;
            ManageRisk.FactoringReapalNo = TravelBatch.FactoringReapalNo;
            ManageRisk.InterestRate = Convert.ToDecimal(TravelBatch.InterestRate);
            ManageRisk.PayCenterCode = TravelBatch.PayCenterCode;
            ManageRisk.PayCenterName = TravelBatch.PayCenterName;
            ManageRisk.TravelBatchId = TravelBatch.TravelBatchId;
            ManageRisk.UserFactoringId = Convert.ToInt32(TravelBatch.UserFactoringId);
            ManageRisk.UserName = TravelBatch.UserName;

            LoggerFactory.Instance.Logger_Debug("ManageRisk实体结果："+JsonConvert.SerializeObject(ManageRisk), "XHCheckTicketNotifyService");
            ViewBaseCheck view = new ViewBaseCheck();
            try
            {
                view = JsonConvert.DeserializeObject<ViewBaseCheck>(data);
                if (view.ErrorRes.Err_code == "200" && view.task[0].status.Trim() == "已完成" && view.task[0].iCount > 0 && view.checkdata[0].success > 0)
                {
                    LoggerFactory.Instance.Logger_Debug("添加小和验证成功日志", "XHCheckTicketNotifyService");
                    //添加小和验证成功日志
                    new XHInterFaceCheckTicketResultLogRep().UpdateXHInterFaceCheckTicketResultLog(view.ErrorRes.uuid, 1);
                    //调用小何验证（小何返回正常结果）
                    XHCheckTicketResult(ManageRisk, view.checkdata[0].samelstDetailed, uuid, true);
                }
                else 
                {
                    //调用小何验证（小何返回信息异常）
                    XHCheckTicketResult(ManageRisk, null, uuid, false);
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                LoggerFactory.Instance.Logger_Debug("小何回调报错：uuid="+uuid+" data="+data, "XHCheckTicketNotifyService");
            }

        }

        
        public void XHCheckTicketResult(ManageRiskModel ManageRisk, List<TicketDetailResult> ResultList, string uuid, bool IsOK)
        {
            string TravelBatchId = ManageRisk.TravelBatchId;
            long EnterpriseID = ManageRisk.EnterpriseID;
            int TravelRiskType = ManageRisk.baseRiskModelList[0].TravelRiskType;
            string strOrderPrice = "";  //票价验证失败票号集合
            string strOrderPersonName = "";//黑屏规则验证成功票号集合
            var travelRisk = new TravelRiskRep().GetTravelRiskByEnterpriseID(new TravelRisk() { EnterpriseID = Convert.ToInt32(ManageRisk.EnterpriseID), PayCenterCode = ManageRisk.PayCenterCode });
            if (IsOK)//小何接口验证成功
            {
                //修改票号集合的小何验证结果状态
                ManageRisk.baseRiskModelList.ForEach(i => i.CheckStatus = 1);
                //根据批次号和企业编号 修改数据库票号集合的小何验证结果状态
                new TravelBatchOrderRep().UpdateTravelBatchOrder(ManageRisk.TravelBatchId, ManageRisk.EnterpriseID, "", "", 0, 0, "", 0, 1, 0, "");

                if (ManageRisk.baseRiskModelList[0].EtermType != 1)//包含小何接口
                {
                    foreach (var item in ManageRisk.baseRiskModelList)
                    {
                        foreach (var result in ResultList)
                        {
                            if (item.TicketNum==result.ticketno)
                            {
                                //验证订单金额
                                if (Convert.ToDecimal(result.fare) * travelRisk.TicketMultiple < item.OrderAmount)//刨除票价验证失败订单（需取表字段配置）
                                {
                                    strOrderPrice += "'" + item.TicketNum + "',";
                                }
                                else if (item.TravelRiskType != 1) //验价成功 且 风控规则不是纯白名单验证的 验证姓名
                                {
                                    if (item.PersonName.Trim() == result.name.Trim())
                                    {
                                        strOrderPersonName += "'" + item.TicketNum + "',";
                                        item.BlackResultState = 1; //设置黑屏验证结果为成功
                                    }
                                }
                                break;
                            }
                        }
                    }  
                }
                //票价验证失败
                if (!string.IsNullOrWhiteSpace(strOrderPrice))
                {
                    strOrderPrice = strOrderPrice.Substring(0, strOrderPrice.Length - 1);
                    ManageRisk.baseRiskModelList.RemoveAll(i => strOrderPrice.Contains(i.TicketNum));
                    //记录验证票价失败的订单
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, strOrderPrice, "", 0, 0, "", 0, 0, 0, "票价验证失败（xh）");
                    //记录验证票价失败的订单日志
                    LoggerFactory.Instance.Logger_Debug("XH验证票价失败的票号:"+strOrderPrice, "XHCheckTicketNotifyService");
                    //清除缓存
                    new RemoneyNotifyService().ModifyTicketCache(new List<string>(strOrderPrice.Substring(1, strOrderPrice.Length - 2).Split("','")));
                }
                //姓名验证
                if (!string.IsNullOrWhiteSpace(strOrderPersonName))
                {
                    strOrderPersonName = strOrderPersonName.Substring(0, strOrderPersonName.Length - 1);
                    //修改黑屏验证状态
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, strOrderPersonName, "", 0, 1, "", 0, 0, 0, "");
                    //修改黑屏验证状态日志
                    LoggerFactory.Instance.Logger_Debug("修改黑屏验证状态为成功:" + strOrderPrice, "XHCheckTicketNotifyService");

                    ManageRisk.baseRiskModelList.FindAll(i => !strOrderPersonName.Contains(i.TicketNum)).ForEach(i => i.FailReason = "姓名验证失败（xh）|");
                    new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, "", strOrderPersonName, 0, 1, "", 0, 0, 0, "姓名验证失败（xh）|");
                }
                if (TravelRiskType != 0 && ManageRisk.baseRiskModelList.Count>0)//判断风控规则不是 纯黑屏 
                {
                    //调用白名单验证
                    new CheckEnterpriseWhiteListService(ManageRisk).Execute();
                }
                //调用王帅核算比率返现接口
                LoggerFactory.Instance.Logger_Debug("调用王帅核算比率返现接口:" + JsonConvert.SerializeObject(ManageRisk), "XHCheckTicketNotifyService");
                try
                {
                    new RemoneyService(ManageRisk).Execute();
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Logger_Debug("返现报错：" + ex.Message, "XHCheckTicketNotifyService");
                }
            }
            else
            {
                switch (ManageRisk.baseRiskModelList[0].EtermType)
                {
                    case 0: //0小何验证
                        //记录验证票价失败的订单(全部)
                        ManageRisk.baseRiskModelList.ForEach(i => i.FailReason = "票价验证失败（xh）|");
                        new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, "", "", 0, 0, "", 0, 0, 0, "票价验证失败（xh）|");
                        //记录验证票价失败的订单日志（全部）
                        LoggerFactory.Instance.Logger_Debug("验证票价失败的订单(全部),批次号：" + TravelBatchId + " ,企业ID：" + EnterpriseID.ToString(), "XHCheckTicketNotifyService");
                        break;
                    case 1: //黑屏
                        //记录黑屏配置错误日志
                        LoggerFactory.Instance.Logger_Debug("黑屏配置错误:"+ ManageRisk.baseRiskModelList[0].EtermType.ToString(), "XHCheckTicketNotifyService");
                        break;
                    case 2: //小何+黑屏
                        //调用黑屏验证
                        new CheckEtermService(ManageRisk).CheckEterm();
                        break;
                    default:
                        //记录风控规则不正确
                        LoggerFactory.Instance.Logger_Debug("风控规则错误:" + ManageRisk.baseRiskModelList[0].EtermType.ToString(), "XHCheckTicketNotifyService");
                        break;
                }
            }
        }

    }
}
