using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CGT.Api.DTO;
using CGT.Api.Service.Boss.UserCenter;

namespace CGT.Api.Controllers.Boss.UserCenter
{
    /// <summary>
    /// 商户中心信息api
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/UserCenter")]
    [EnableCors("AllowSameDomain")]
    public class UserCenterController : BaseController
    {
        public QueryMerchatsInfoService queryMerchatsInfoService { get; set; }

        public ModifyMerchatsInfoService modifyMerchatsInfoService { get; set; }

        public ModifyMerchatsStatusService modifyMerchatsStatusService { get; set; }
        public UpdateMerchatsInfoForJavaService updateMerchatsInfoForJavaService { get; set; }

        public UpdateMerchatsStatusForJavaService updateMerchatsStatusForJavaService { get; set; }
        public GoldResgistForJavaService goldResgistForJavaService { get; set; }

        public GoldEditInterestRateForJavaService goldEditInterestRateForJavaService { get; set; }

        public RegisteredAgentService registeredAgentService { get; set; }
        public RegisteredUserService registeredUserService { get; set; }

        /// <summary>
        /// 商户查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("queryMerchatsInfo"), HttpPost]
        public async Task<ResponseMessageModel> QueryMerchatsInfo([FromBody] RequestModel model)
        {
            queryMerchatsInfoService.SetData(model);
            return await Task.Run(() => queryMerchatsInfoService.Execute());
        }
        /// <summary>
        /// 商户信息修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("modifyMerchatsInfo"), HttpPost]
        public async Task<ResponseMessageModel> ModifyMerchatsInfo([FromBody] RequestModel model)
        {
            modifyMerchatsInfoService.SetData(model);
            return await Task.Run(() => modifyMerchatsInfoService.Execute());
        }
        /// <summary>
        /// 商户状态修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("modifyMerchatsStatus"), HttpPost]
        public async Task<ResponseMessageModel> ModifyMerchatsStatus([FromBody] RequestModel model)
        {
            modifyMerchatsStatusService.SetData(model);
            return await Task.Run(() => modifyMerchatsStatusService.Execute());
        }
        /// <summary>
        /// 商户修改（java用）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("updateMerchatInfoForJava"),HttpPost]
        public async Task<ResponseMessageModel> UpdateMerchatsInfoForJava([FromBody] RequestModel model)
        {
            updateMerchatsInfoForJavaService.SetData(model);
            return await Task.Run(() => updateMerchatsInfoForJavaService.Execute());
        }
        /// <summary>
        /// 商户启用状态修改（java用）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("updateMerchatsStatusForJava"),HttpPost]
        public async Task<ResponseMessageModel> UpdateMerchatsStatusForJava([FromBody] RequestModel model)
        {
            updateMerchatsStatusForJavaService.SetData(model);
            return await Task.Run(() => updateMerchatsStatusForJavaService.Execute());
        }
        /// <summary>
        /// 金主注册(java用)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("goldResgistForJava"),HttpPost]
        public async Task<ResponseMessageModel> GoldResgistForJava([FromBody] RequestModel model)
        {
            goldResgistForJavaService.SetData(model);
            return await Task.Run(() => goldResgistForJavaService.Execute());
        }

        /// <summary>
        /// 金主注册(java用)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("goldEditInterestRateForJava"),HttpPost]
        public async Task<ResponseMessageModel> GoldEditInterestRateForJava([FromBody] RequestModel model)
        {
            goldEditInterestRateForJavaService.SetData(model);
            return await Task.Run(() => goldEditInterestRateForJavaService.Execute());
        }

        /// <summary>
        /// 代理注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("registeredAgent"),HttpPost]
        public async Task<ResponseMessageModel> RegisteredAgent([FromBody] RequestModel model)
        {
            registeredAgentService.SetData(model);
            return await Task.Run(() => registeredAgentService.Execute());
        }

        /// <summary>
        /// 分销注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("registeredUser"),HttpPost]
        public async Task<ResponseMessageModel> RegisteredUser([FromBody] RequestModel model)
        {
            registeredUserService.SetData(model);
            return await Task.Run(() => registeredUserService.Execute());
        }
    }
}
