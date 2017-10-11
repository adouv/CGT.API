using CGT.Api.DTO;
using CGT.Api.Service.Manage.Account;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Controllers.Manage.Account
{
    /// <summary>
    /// 分销平台差旅企业信息API
    /// </summary>
    [Produces("application/json")]
    [Route("api/Manage/Account")]
    [EnableCors("AllowSameDomain")]
    public class AccountController : BaseController
    {
        #region 注入服务

        public GetAccountBalanceService GetAccountBalanceSer { get; set; }


        #endregion

        /// <summary>
        /// 获取商户账户余额
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("AccountBalance"), HttpPost]
        public ResponseMessageModel GetAccountBalance([FromBody]RequestModel model)
        {
            GetAccountBalanceSer.SetData(model);
            return GetAccountBalanceSer.Execute();
        }
    }
}
