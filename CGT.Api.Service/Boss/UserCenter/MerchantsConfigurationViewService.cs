using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.UserCenter.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 商户配置详情
    /// </summary>
    public class MerchantsConfigurationViewService : ApiBase<RequestMerchantsConfigurationViewModel>
    {
        public MerchantsConfigurationViewProcessor merchantsConfigurationViewProcessor { get; set; }
        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod()
        {
            merchantsConfigurationViewProcessor.Init(
                this.Parameter.companyCode);
            var data = merchantsConfigurationViewProcessor.Execute();
            this.Result.Data = data.Result;
        }
    }
}
