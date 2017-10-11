using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CGT.Api.DTO;
using CGT.Api.Service.Boss.UserCenter;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CGT.Api.Controllers.Boss.UserCenter
{
    /// <summary>
    /// 商户配置信息api
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/UserCenter")]
    [EnableCors("AllowSameDomain")]
    public class MerchantsConfigurationController : BaseController
    {
        #region 服务注入
        public MerchantsConfigurationQueryService merchantsConfigurationQueryService { get; set; }
        public MerchantsConfigurationViewService merchantsConfigurationViewService { get; set; }
        public MerchantsConfigurationSaveService merchantsConfigurationSaveService { get; set; }
        public MerchantsConfigurationUpdateService merchantsConfigurationUpdateService { get; set; }
        #endregion

        /// <summary>
        /// 商户配置查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("MerchantsConfigurationQuery"), HttpPost]
        public async Task<ResponseMessageModel> MerchantsConfigurationQuery([FromBody] RequestModel model)
        {
            merchantsConfigurationQueryService.SetData(model);
            return await Task.Run(() => merchantsConfigurationQueryService.Execute());
        }
        /// <summary>
        /// 商户配置详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("MerchantsConfigurationView"),HttpPost]
        public async Task<ResponseMessageModel> MerchantsConfigurationView([FromBody] RequestModel model)
        {
            merchantsConfigurationViewService.SetData(model);
            return await Task.Run(() => merchantsConfigurationViewService.Execute());
        }
        /// <summary>
        /// 商户配置修改(JAVA)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("MerchantsConfigurationSave"), HttpPost]
        public async Task<ResponseMessageModel> MerchantsConfigurationSave([FromBody]RequestModel model)
        {                   
            merchantsConfigurationSaveService.SetData(model);
            return await Task.Run(() => merchantsConfigurationSaveService.Execute());
        }
        /// <summary>
        /// 商户配置修改(NET)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("MerchantsConfigurationUpdate"), HttpPost]
        public async Task<ResponseMessageModel> MerchantsConfigurationUpdate([FromBody]RequestModel model)
        {
            merchantsConfigurationUpdateService.SetData(model);
            return await Task.Run(() => merchantsConfigurationUpdateService.Execute());
        }
    }
}
