using System;
using System.Collections.Generic;
using System.Text;
using CGT.Api.Service.Boss.TravelUserFactoring;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CGT.Api.DTO;
using System.Threading.Tasks;

namespace CGT.Api.Controllers.Boss.Travel
{
    /// <summary>
    /// boss差旅风控api
    /// </summary>
    [Produces("application/json")]
    [Route("api/boss/traveluserfactoring")]
    [EnableCors("AllowSameDomain")]
    public class TravelUserFactoringController : BaseController
    {
        /// <summary>
        /// 差旅金主列表服务
        /// </summary>
        public GetTravelUserFactoringListService getTravelUserFactoringListService { get; set; }
        /// <summary>
        /// 获取差旅金主列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("GetTravelUserFactoringList"), HttpPost]
        public async Task<ResponseMessageModel> GetTravelUserFactoringList([FromBody]RequestModel model)
        {
            getTravelUserFactoringListService.SetData(model);
            return await Task.Run(() => getTravelUserFactoringListService.Execute());
        }
    }
}
