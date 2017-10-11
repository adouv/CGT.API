using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.PetaPoco.Repositories.Cgt;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 商户配置修改提示
    /// </summary>
    public class MerchantsConfigurationUpdateService : ApiBase<RequestMerchantsConfigurationUpdateModel>
    {
        public UserAccountRep userAccountRep { get; set; }

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
            var result = userAccountRep.UpdateMerchantsConfigurationByFactoringCode(
                this.Parameter.travelRate,
                this.Parameter.accountCode,
                this.Parameter.companyCode);
            if (result > 0)
            {
                this.Result.IsSuccess = true;
            }
            else
            {
                throw new Exception("操作失败");
            }
        }
    }
}
