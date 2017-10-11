using CGT.Api.DTO;
using CGT.Api.DTO.Boss.User;
using CGT.Entity.CgtModel;
using CGT.PetaPoco.Repositories.Cgt;
using System;

namespace CGT.Api.Service.Boss.User {
    /// <summary>
    /// 修改分销信息
    /// </summary>
    public class UpdateUserAccountService : ApiBase<RequestUpdateUserAccount> {
        #region 注入服务
        public UserAccountRep userAccountRep { get; set; }

        #endregion
        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json) {
            base.SetData(json);
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate() {
            base.Validate();
            if (string.IsNullOrWhiteSpace(Parameter.PayCenterCode) && string.IsNullOrWhiteSpace(Parameter.MerchantCode)) {
                throw new AggregateException("分销Code或商户Code不能为空！");
            }
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            var model = new UserAccount() {
                PayCenterCode = Parameter.PayCenterCode,
                GraceDay = Parameter.GraceDay,
                ModifyName = Parameter.ModifyName,
                GraceBate = Parameter.GraceBate,
                OverdueBate = Parameter.OverdueBate,
                MerchantCode = Parameter.MerchantCode,
                CreditAmount = Parameter.CreditAmount,
                FactoringInterestRate = Parameter.FactoringInterestRate,
            };
            var data = userAccountRep.UpdateUserAccount(model);
            if (data > 0) {
                this.Result.Data = data;
            }
            else {
                throw new AggregateException("分销信息修改失败");
            }
        }
    }
}
