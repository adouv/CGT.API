using CGT.Api.DTO;
using CGT.Api.Service.Boss.TravelRisk;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CGT.Api.Controllers.Boss.Travel
{
    /// <summary>
    /// boss差旅订单api
    /// </summary>
    [Produces("application/json")]
    [Route("api/boss/xhinterface")]
    [EnableCors("AllowSameDomain")]
    public class XHInterFaceCheckTicketResultController : BaseController
    {
        #region 注入服务

        public GetXHInterFaceCheckTicketResultLogListService getTravelOrderListService { get; set; }

        #endregion

        /// <summary>
        /// 获取差旅订单详情
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("list"), HttpPost]
        public async Task<ResponseMessageModel> GetXHInterFaceCheckTicketResultLogList([FromBody]RequestModel model)
        {
            getTravelOrderListService.SetData(model);
            return await Task.Run(() => getTravelOrderListService.Execute());
        }
    }
}
