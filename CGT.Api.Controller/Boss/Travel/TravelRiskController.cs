using CGT.Api.DTO;
using CGT.Api.Service.Boss.TravelRisk;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CGT.Api.Controllers.Boss.Travel {
    /// <summary>
    /// boss差旅风控api
    /// </summary>
    [Produces("application/json")]
    [Route("api/boss/travelrisk")]
    [EnableCors("AllowSameDomain")]
    public class TravelRiskController : BaseController {
        #region 注入服务
        public AddTravelRiskService addTravelRiskService { get; set; }
        public GetTravelRiskListService getTravelRiskListService { get; set; }

        #endregion
        /// <summary>
        /// 添加风控规则列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("add"), HttpPost]
        public ResponseMessageModel AddTravelRisk([FromBody]RequestModel model) {
            addTravelRiskService.SetData(model);
            return addTravelRiskService.Execute();
        }
        /// <summary>
        /// 查询风控规则列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("list"), HttpPost]
        public ResponseMessageModel getTravelRiskList([FromBody]RequestModel model) {
            getTravelRiskListService.SetData(model);
            return getTravelRiskListService.Execute();
        }
    }

}
