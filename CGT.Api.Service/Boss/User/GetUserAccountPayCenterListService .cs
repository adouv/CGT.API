using CGT.Api.DTO;
using CGT.Api.DTO.Boss.User;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.Entity.CgtModel;


namespace CGT.Api.Service.Boss.User {
    /// <summary>
    /// 分销商限额列表
    /// </summary>
    public class GetUserAccountPayCenterListService : ApiBase<RequestGetUserAccountList> {
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
            //var model = new UserAccount()
            //{
            //    UserType = this.Parameter.UserType ?? 3,
            //    PayCenterCode = this.Parameter.PayCenterCode
            //};
            var data = userAccountRep.GetUserAccountPagyCenterList();
            this.Result.Data = data;
        }
    }
}
