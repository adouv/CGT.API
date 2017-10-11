using System;
using System.Collections.Generic;
using System.Text;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using CGT.Event.Model.Manage;
using CGT.Api.Service.Manage.Remoney;
using CGT.DDD.Logger;
using Newtonsoft.Json;

namespace CGT.Api.Service.Manage.CheckTicket
{
    /// <summary>
    /// 企业白名单验证服务类
    /// </summary>
    public class CheckEnterpriseWhiteListService : CheckTicketBaseService
    {
        /// <summary>
        /// 批次子表集合
        /// </summary>
        public ManageRiskModel ManageRisk = new ManageRiskModel();

        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model"></param>
        public CheckEnterpriseWhiteListService(ManageRiskModel model) : base() {
            ManageRisk = model;
        }

        /// <summary>
        /// 验证
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }

        /// <summary>
        /// 执行调用白名单验证
        /// </summary>
        public override void Execute()
        {
            string strWhiteSuccess = "";
            string TravelBatchId = ManageRisk.TravelBatchId;
            long EnterpriseID = ManageRisk.EnterpriseID;
            //获取该企业员工白名单集合
            List<EnterpriseStaff> StaffList = new EnterpriseStaffRep().QueryStaffList(new EnterpriseStaff() { PayCenterCode = ManageRisk.PayCenterCode, EnterpriseId = ManageRisk.EnterpriseID });
            //企业票号乘机人和企业白名单姓名对比
            foreach (var item in ManageRisk.baseRiskModelList)
            {
                foreach (var Staff in StaffList)
                {
                    if (item.PersonName.Trim()==Staff.StaffName.Trim())
                    {
                        item.WhiteResultState = 1;//修改白名单验证结果（实体)
                        strWhiteSuccess +="'" + item.TicketNum + "',";
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(strWhiteSuccess))
            {
                strWhiteSuccess = strWhiteSuccess.Substring(0,strWhiteSuccess.Length-1);
                //修改白名单验证结果（数据库）根据批次号和票号
                new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, strWhiteSuccess, "", 1, 0, "", 0, 0, 0,"");
                LoggerFactory.Instance.Logger_Debug("白名单验证成功："+strWhiteSuccess, "CheckEnterpriseWhiteListService");

                //员工白名单验证失败
                ManageRisk.baseRiskModelList.FindAll(i => !strWhiteSuccess.Contains(i.TicketNum)).ForEach(i => i.FailReason = "|员工姓名验证失败（bmd）");
                new TravelBatchOrderRep().UpdateTravelBatchOrder(TravelBatchId, EnterpriseID, "", strWhiteSuccess, 1, 0, "", 0, 0, 0, "|员工姓名验证失败（bmd）");
            }

            if (ManageRisk.baseRiskModelList[0].EtermType==-1)
            {
                //调用王帅计算比率返现方法
                LoggerFactory.Instance.Logger_Debug("调用王帅计算比率返现,list集合："+ JsonConvert.SerializeObject(ManageRisk), "CheckEnterpriseWhiteListService");
                try
                {
                    new RemoneyService(ManageRisk).Execute();
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Logger_Debug("返现报错：" + ex.Message, "CheckEnterpriseWhiteListService");
                }
            }
        }
    }
}
