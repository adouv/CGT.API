using CGT.Api.DTO;
using CGT.Api.Service.Boss.TravelBatch;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CGT.Api.Controllers.Boss.Travel
{
    /// <summary>
    /// 差旅风控日志API
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/TravelBatch")]
    [EnableCors("AllowSameDomain")]
    public class TravelBatchController : Controller
    {
        #region 注入服务
        public GetTravelBatchService batchService { get; set; }
        public GetTravelBatchOrderService batchorderService { get; set; }
        #endregion

        /// <summary>
        /// 风控批次数据
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("TravelRiskLog"), HttpPost]
        public ResponseMessageModel GetTravelRiskLogList([FromBody]RequestModel model)
        {
            batchService.SetData(model);
            return batchService.Execute();
        }
        /// <summary>
        /// 订单汇总详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("TravelBatchOrder"),HttpPost]
        public ResponseMessageModel GetTravelBatchOrder([FromBody]RequestModel model)
        {
            batchorderService.SetData(model);
            return batchorderService.Execute();
        }
    }
}
