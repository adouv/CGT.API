using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelOrder;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;

namespace CGT.Api.Service.Boss.TravelOrder
{
    /// <summary>
    /// 差旅订单下载
    /// </summary>
    public class GetTravelOrderDataService : ApiBase<RequestDownloadTravelOrderList>
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
            var model = new FactoringOrderModel()
            {
                BillId = this.Parameter.BillId,
                BeginBillDate = this.Parameter.StartBillDate,
                EndBillDate = this.Parameter.EndBillDate,
                EnterpriseId = this.Parameter.EnterpriseId,
                EnterpriseName = this.Parameter.EnterpriseName,
                MerchantCode = this.Parameter.MerchantCode,
                RepaymentStatus = this.Parameter.Status,
            };
           var list = enterpriseOrderRep.GetEnterpriseOrderData(model, this.Parameter.pageindex, this.Parameter.pagesize);
            this.Result.Data = list;
        }
    }
}
