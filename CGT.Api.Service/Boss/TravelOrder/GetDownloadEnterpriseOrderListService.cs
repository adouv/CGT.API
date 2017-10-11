using CGT.Api.DTO;
using CGT.DDD.Utils;
using CGT.DDD.Logger;
using CGT.Api.DTO.Boss.TravelOrder;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Text;
using CGT.Entity.CgtTravelModel;

namespace CGT.Api.Service.Boss.TravelOrder
{
    public class GetDownloadEnterpriseOrderListService : ApiBase<RequestTravelOrderList>
    {
        #region 注入服务
        public EnterpriseOrderRep enterpriseOrderRep { get; set; }
        public ExeclHelper Excel { get; set; }
        #endregion
        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod()
        {
            try
            {
                var result = enterpriseOrderRep.GetDownloadEnterpriseOrderList(
                    Parameter.EnterpriseID, 
                    Parameter.PayCenterCode, 
                    Parameter.OrderId, 
                    Parameter.TravelBatchId, 
                    Parameter.TicketTimeBegion, 
                    Parameter.TicketTimeEnd?.AddDays(1).AddSeconds(-1), 
                    Parameter.BackTimeBegion, 
                    Parameter.BackTimeEnd?.AddDays(1).AddSeconds(-1), 
                    Parameter.ReviewState, 
                    Parameter.ReviewTimeBegion, 
                    Parameter.ReviewTimeEnd?.AddDays(1).AddSeconds(-1), 
                    Parameter.TravelRiskState, 
                    Parameter.TravelRiskType, 
                    Parameter.BackStatus);

                if (result.Count > 0 && result != null)
                {
                    List<string> titlelist = new List<string>();
                    titlelist.Add("批次号");
                    titlelist.Add("订单号");
                    titlelist.Add("分销商名称");
                    titlelist.Add("企业名称");
                    titlelist.Add("返现金额");
                    titlelist.Add("返现状态");

                    List<string> keylist = new List<string>();
                    keylist.Add("TravelBatchId");
                    keylist.Add("OrderId");
                    keylist.Add("PayCenterName");
                    keylist.Add("EnterpriseName");
                    keylist.Add("TicketAmount");
                    keylist.Add("BackStatusStr");

                    var filebytes = Excel.ExcelExport<EnterpriseOrder>(result, titlelist, keylist);
                    Result.Data = Convert.ToBase64String(filebytes);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "GetTravelBillDownloadServiceError");
                throw new AggregateException(ex.Message);
            }
        }
    }
}
