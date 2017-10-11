using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.Entity.CgtModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.UserCenter.Service;
using Org.BouncyCastle.Utilities.IO;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 商户修改（java用）
    /// </summary>
    public class UpdateMerchatsStatusForJavaService : ApiBase<RequsetMerchatsModifyStatus>
    {
        public InterfaceAccountRep interfaceRep { get; set; }
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
            var result = interfaceRep.UpdateInterfaceAccountStatus(
                 new InterfaceAccount() { Status = this.Parameter.status, MerchantCode = Parameter.companyCode });
            if (result > 0)
            {
                this.Result.IsSuccess = true;
            }
            else
            {
                throw new System.Exception("更新数据库失败");
            }
        }
    }
}
