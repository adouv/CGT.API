using CGT.Api.DTO;
using CGT.Api.Service.Boss.CgtOrder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CGT.Api.Controllers.Boss.CgtOrder {
    /// <summary>
    /// 采购通国际票订单
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/CgtOrder")]
    [EnableCors("AllowSameDomain")]
    public class CgtOrderController : BaseController {
        public CgtInterOrderService cgtInterOrderService { get; set; }

        public CgtInterOrderDetailService cgtInterOrderDetailService { get; set; }

        /// <summary>
        /// 国际票订单列表
        /// </summary>
        /// <param name="model">Data{}</param>
        /// <returns></returns>
        [Route("InterOrderList"), HttpPost]
        public ResponseMessageModel GetInterOrderList([FromBody]RequestModel model) {
            cgtInterOrderService.SetData(model);
            return cgtInterOrderService.Execute();
        }
        /// <summary>
        /// 国际票订单详单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("InterOrderDetail"), HttpPost]
        public ResponseMessageModel GetInterOrderDetail([FromBody]RequestModel model) {
            cgtInterOrderDetailService.SetData(model);
            return cgtInterOrderDetailService.Execute();
        }
    }

}
