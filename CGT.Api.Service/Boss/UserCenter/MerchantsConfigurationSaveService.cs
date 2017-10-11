using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.UserCenter.Service;
using CGT.PetaPoco.Repositories.Cgt;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 商户配置修改
    /// </summary>
    public class MerchantsConfigurationSaveService : ApiBase<RequestMerchantsConfigurationSaveModel>
    {
        public MerchantsConfigurationSaveProcessor merchantsConfigurationSaveProcessor { get; set; }
        
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
            merchantsConfigurationSaveProcessor.Init(
                this.Parameter.loginName,
                this.Parameter.companyCode,
                this.Parameter.accountCode,
                this.Parameter.accountType,
                this.Parameter.accountBusiType,
                this.Parameter.busiType,
                this.Parameter.creditAmount,
                this.Parameter.billDays,
                this.Parameter.totalCreditAmount,
                (this.Parameter.travelRate/100).ToString());
            var data = merchantsConfigurationSaveProcessor.Execute();
            this.Result.Data = data.Message.Trim();
        }
    }
}
