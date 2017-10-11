using CGT.Api.DTO;
using CGT.Api.DTO.Boss.InterRefund;
using CGT.Entity.CgtTicketModel;
using CGT.PetaPoco.Repositories.CgtTicket;
using Microsoft.Extensions.Caching.Memory;

namespace CGT.Api.Service.Boss.CgtOrder
{
    /// <summary>
    /// 国际订单
    /// </summary>
    public class CgtInterOrderService : ApiBase<RequestInterRefund>
    {
        #region 注入仓储
        public OrderRep orderRep { get; set; }
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
            var model = new Order()
            {
                OrderId = this.Parameter.OrderId,
                LocalId = this.Parameter.LocalId,
                TravelType=1,//国际票
                 Status = this.Parameter.Status
            };
            this.Result.Data = orderRep.GetOrderPage(this.Parameter.PageIndex,
                this.Parameter.PageSize, this.Parameter.StartDate, this.Parameter.EndDate,
                model);
        }
    }
}
