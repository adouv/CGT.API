using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelRisk;
using CGT.DDD.Logger;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using Newtonsoft.Json;
using System;

namespace CGT.Api.Service.Boss.TravelRisk {
    /// <summary>
    /// 添加风控规则
    /// </summary>
    public class AddTravelRiskService : ApiBase<RequsetAddTravelRisk> {
        #region 注入服务
        public TravelRiskRep travelRiskRep { get; set; }
        public EnterpriseWhiteRep enterpriseWhiteRep { get; set; }
        #endregion
        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json) {
            base.SetData(json);
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate() {
            base.Validate();
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            if (Parameter.TravelRiskId == 0) {
                var model = new Entity.CgtTravelModel.TravelRisk() {
                    CreateTime = DateTime.Now,
                    CreateUserId = Parameter.CreateUserId,
                    EnterpriseID = Parameter.EnterpriseID,
                    EtermFailRate = Parameter.EtermFailRate,
                    EtermSuccessRate = Parameter.EtermSuccessRate,
                    TravelRiskState = Parameter.TravelRiskState,
                    TravelRiskType = Parameter.TravelRiskType,
                    WhiteFailRate = Parameter.WhiteFailRate,
                    WhiteSuccessRate = Parameter.WhiteSuccessRate,
                    PayCenterCode = Parameter.PayCenterCode,
                    UploadLowCount = Parameter.UploadLowCount,
                    PayCenterName = Parameter.PayCenterName,
                    EtermType = 2,
                    TicketMultiple = Parameter.TicketMultiple
                };
                //判断企业风控规则启动是否存在
                var travelRisk = travelRiskRep.GetTravelRiskByEnterpriseID(model);
                if (travelRisk != null && Parameter.TravelRiskState == 1)
                    throw new AggregateException("此企业已有开启风控规则！");
                //批量添加
                if (Parameter.EnterpriseID <= 0) {
                    var enterprisemodel = new EnterpriseWhiteList() {
                        PayCenterCode = this.Parameter.PayCenterCode
                    };
                    var data = enterpriseWhiteRep.GetEnterpriseWhiteList(enterprisemodel);
                    foreach (var item in data) {
                        model.EnterpriseID = (int)item.EnterpriseWhiteListID;
                        var res = travelRiskRep.AddTravelRisk(model);
                        if (res > 0) {
                            LoggerFactory.Instance.Logger_Info(JsonConvert.SerializeObject(model), "TravelRiskLog");
                            this.Result.Data = res;
                        }
                        else {
                            throw new AggregateException("保存风控规则失败！");
                        }
                    }
                }
                //单个添加
                else {
                    var res = travelRiskRep.AddTravelRisk(model);
                    if (res > 0) {
                        LoggerFactory.Instance.Logger_Info(JsonConvert.SerializeObject(model), "TravelRiskLog");
                        this.Result.Data = res;
                    }
                    else {
                        throw new AggregateException("保存风控规则失败！");
                    }
                }
            }
            else {
                var model = new Entity.CgtTravelModel.TravelRisk() {
                    ModifyUserId = Parameter.ModifyUserId,
                    ModifyTime = DateTime.Now,
                    EtermFailRate = Parameter.EtermFailRate,
                    EtermSuccessRate = Parameter.EtermSuccessRate,
                    TravelRiskState = Parameter.TravelRiskState,
                    TravelRiskType = Parameter.TravelRiskType,
                    WhiteFailRate = Parameter.WhiteFailRate,
                    WhiteSuccessRate = Parameter.WhiteSuccessRate,
                    TravelRiskId = Parameter.TravelRiskId,
                    UploadLowCount = Parameter.UploadLowCount,
                    EnterpriseID = Parameter.EnterpriseID,
                    PayCenterCode = Parameter.PayCenterCode,
                    TicketMultiple = Parameter.TicketMultiple
                };
                //判断企业风控规则启动是否存在
                var travelRisk = travelRiskRep.GetTravelRiskByEnterpriseID(model);
                if (travelRisk != null && Parameter.TravelRiskState == 1 && travelRisk.TravelRiskId != Parameter.TravelRiskId)
                    throw new AggregateException("此企业已有开启风控规则！");
                //修改风控规则
                var res = travelRiskRep.UpdateTravelRisk(model);
                if (res > 0) {
                    LoggerFactory.Instance.Logger_Info(JsonConvert.SerializeObject(model), "TravelRiskLog");
                    this.Result.Data = res;
                }
                else {
                    throw new AggregateException("保存风控规则失败！");
                }
            }
        }
    }
}
