using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.PetaPoco.Repositories.CgtTravel;

namespace CGT.Api.Service.Boss.Enterprise {
    /// <summary>
    /// 获取提交审核企业列表
    /// </summary>
    public class GetEnterpriseListService : ApiBase<RequestGetEnterpriseList> {
        #region 注入仓储
        public EnterpriseTempoaryRep EpRep { get; set; }
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
        protected override void ExecuteMethod() {
            var result = EpRep.GetPageList(this.Parameter.Pageindex, this.Parameter.Pagesize, this.Parameter.UserId);
            this.Result.Data = result;
        }
    }
}
