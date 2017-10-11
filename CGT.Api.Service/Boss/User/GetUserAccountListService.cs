using CGT.Api.DTO;
using CGT.Api.DTO.Boss.User;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.Entity.CgtModel;

namespace CGT.Api.Service.Boss.User {
    /// <summary>
    /// 获取分销用户列表
    /// </summary>
    public class GetUserAccountListService : ApiBase<RequestGetUserAccountList> {
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
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            var model = new UserAccount() {
                UserType = this.Parameter.UserType ?? 3,
                PayCenterCode = this.Parameter.PayCenterCode
            };
            var data = userAccountRep.GetUserAccountList(model,Parameter.ModifyBeginTime, Parameter.ModifyEndTime?.AddDays(1).AddMinutes(-1), Parameter.PageIndex, Parameter.PageSize);
            this.Result.Data = data;
        }
    }
}
