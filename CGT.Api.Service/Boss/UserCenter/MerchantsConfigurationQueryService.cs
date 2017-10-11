using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.UserCenter.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 商户配置查询
    /// </summary>
    public class MerchantsConfigurationQueryService : ApiBase<RequestMerchantsConfigurationModel>
    {
        public MerchantsConfigurationQueryProcessor merchantsConfigurationQueryProcessor { get; set; }
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
            merchantsConfigurationQueryProcessor.Init(
                this.Parameter.companyName,
                this.Parameter.status,
                this.Parameter.currentPage,
                this.Parameter.pageSize,
                this.Parameter.startDate,
                this.Parameter.endDate);
            var data = merchantsConfigurationQueryProcessor.Execute();
            this.Result.Data = data.Result;
        }
    }
}
