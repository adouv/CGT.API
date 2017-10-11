using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;

namespace CGT.Api.Service.Boss.Enterprise
{
    /// <summary>
    /// 获取企业白名单列表
    /// </summary>
    public class GetEnterpriseWhiteListService : ApiBase<RequestGetWhiteEnterpriseList>
    {
        #region 注入服务
        public EnterpriseWhiteRep enterpriseWhiteRep { get; set; }
        #endregion

        /// <summary>
        /// Api赋值
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
            var model = new EnterpriseWhiteList()
            {
                PayCenterCode = this.Parameter.PayCenterCode
            };
            var data = enterpriseWhiteRep.GetEnterpriseWhiteList(model);
            this.Result.Data = data;
        }
    }
}
