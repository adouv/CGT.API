using CGT.Api.DTO;
using CGT.Api.DTO.Manage.Remoney;
using CGT.Api.Service.Manage.Remoney;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CGT.Api.Controllers.Notify {
    /// <summary>
    /// 差旅批量返现回调
    /// </summary>
    [Produces("application/json")]
    [Route("api/manage/remoney")]
    [EnableCors("AllowSameDomain")]
    public class RemoneyNotifyController : BaseController {
        /// <summary>
        ///  批量返现回掉通知通知类
        /// </summary>
        RemoneyNotifyService remoneyNotifyService = new RemoneyNotifyService();
        /// <summary>
        /// 回调验证结果
        /// </summary>
        /// <param name="model">小何json数据集</param>
        /// <returns></returns>
        [Route("notify"), HttpPost]
        public async Task<ResponseMessageModel> RemoneyNotify([FromBody] RequestRemoneyModel model) {
            return await Task.Run(() => remoneyNotifyService.Execute(model));
        }
    }
}
