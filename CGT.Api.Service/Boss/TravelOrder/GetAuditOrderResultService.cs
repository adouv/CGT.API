using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelOrder;
using CGT.Api.DTO.Boss.TravelOrder.Response;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.TravelOrder
{
    public class GetAuditOrderResultService : ApiBase<RequestTravelOrderAuditResult>
    {
        #region 注入

        public EnterpriseOrderRep EnterpriseOrderRep { get; set; }

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
            Result.Data = EnterpriseOrderRep.GetTravelAuditResult(Parameter.EnterpriseId, Parameter.PayCenterCode, Parameter.TravelBatchId,
            Convert.ToDateTime(Parameter.StartDate), Convert.ToDateTime(Parameter.EndDate), Parameter.Pageindex, Parameter.Pagesize);
            ;
        }
    }
}
