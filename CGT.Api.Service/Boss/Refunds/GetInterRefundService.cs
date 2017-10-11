using CGT.Api.DTO;
using CGT.Api.DTO.Boss.InterRefund;
using CGT.Entity.CgtTicketModel;
using CGT.PetaPoco.Repositories.CgtTicket;

namespace CGT.Api.Service.Boss.Refunds
{
    /// <summary>
    ///  国际票退票列表
    /// </summary>
    public class GetInterRefundService : ApiBase<RequestInterRefund>
    {
        #region 注入仓储
        public InterRefundRep interRefundRep { get; set; }
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

        protected override void ExecuteMethod()
        {
            var result = interRefundRep.GetInterRefundPage(this.Parameter.PageIndex,
                this.Parameter.PageSize,this.Parameter.StartDate,this.Parameter.EndDate,
                new InterRefund
                {
                    OrderID = this.Parameter.LocalId,
                    OrderOrderId = this.Parameter.OrderId,
                    AffairStatus=this.Parameter.AffairStatus,
                    Status=this.Parameter.Status
                });
            this.Result.Data = result;
        }
    }
}
