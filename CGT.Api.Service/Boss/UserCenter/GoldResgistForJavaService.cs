using System;
using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.Entity.CgtModel;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTravel;
using CGT.UserCenter.Service;
using Org.BouncyCastle.Utilities.IO;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 金主注册（java用）
    /// </summary>
    public class GoldResgistForJavaService : ApiBase<RequsetGoldInfo>
    {
        #region 注入服务

         public  UserFactoringRep userFactoringRep { set; get; }
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
           userFactoringRep.InsertUserFactoring(new UserFactoring()
           {
               FactoringCode = this.Parameter.accountCode,
               FactoringEmail = this.Parameter.loginName,
               FactoringName = this.Parameter.accountName,
               FactoringReapalNo = this.Parameter.merchantNo,
               CreateTime = DateTime.Now
           });
        }
    }
}
