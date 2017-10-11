using CGT.Api.DTO;
using CGT.Api.Service.Boss.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CGT.Api.Controllers.Boss.User
{
    /// <summary>
    /// boss用户信息api
    /// </summary>
    [Produces("application/json")]
    [Route("api/boss/User")]
    [EnableCors("AllowSameDomain")]
    public class UserController : BaseController {
        #region 注入服务
        public GetUserAccountListService getUserAccountListService { get; set; }

        public UpdateUserAccountService  updateUserAccountService {get;set;}

        public GetUserAccountLimitListService getUserAccountLimitListService { get; set; }

        public GetUserAccountPayCenterListService getUserAccountPayCenterListService { get; set; }
        #endregion
        /// <summary>
        /// 获取分销用户列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("UserList"), HttpPost]
        public ResponseMessageModel GetUserAccountList([FromBody]RequestModel model) {
            getUserAccountListService.SetData(model);
            return getUserAccountListService.Execute();
        }
        /// <summary>
        /// 获取分销用户差旅授信余额列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("UserAccountLimitList"), HttpPost]
        public ResponseMessageModel GetUserAccountLimitList([FromBody]RequestModel model) {
            getUserAccountLimitListService.SetData(model);
            return getUserAccountLimitListService.Execute();
        }
        /// <summary>
        /// 获取分销用户列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("UpdateUser"), HttpPost]
        public ResponseMessageModel UpdateUserAccount([FromBody]RequestModel model) {
            updateUserAccountService.SetData(model);
            return updateUserAccountService.Execute();
        }
        /// <summary>
        /// 获取分销用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("UserAccountPayCenterList"), HttpPost]
        public ResponseMessageModel GetUserAccountPayCenterList([FromBody] RequestModel model)
        {
            getUserAccountPayCenterListService.SetData(model);
            return getUserAccountPayCenterListService.Execute();
        }
    }
}
