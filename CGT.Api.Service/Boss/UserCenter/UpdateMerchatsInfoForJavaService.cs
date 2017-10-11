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
    public class UpdateMerchatsInfoForJavaService : ApiBase<RequsetMerchatsModifyInfo>
    {
        #region 注入服务
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
           
        }
    }
}
