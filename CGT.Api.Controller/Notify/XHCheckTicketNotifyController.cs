using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CGT.Api.Service.Manage.CheckTicket;
using CGT.Api.DTO;

namespace CGT.Api.Controllers.Notify {
    /// <summary>
    /// 小何验票接口回调api
    /// </summary>
    [Produces("application/json")]
    [Route("api/manage/xhcheckticket")]
    [EnableCors("AllowSameDomain")]
    public class XHCheckTicketNotifyController : BaseController {
        /// <summary>
        /// 回调验证结果
        /// </summary>
        /// <param name="uuid">小何批次号</param>
        /// <param name="data">小何json数据集</param>
        /// <returns></returns>
        [Route("notify"), HttpPost]
        public async Task<string> GetTravelOrder(string uuid, string data) {

            await Task.Run(() => new XHCheckTicketNotifyService(uuid, data).Execute());
            return "success";
        }
    }
}
