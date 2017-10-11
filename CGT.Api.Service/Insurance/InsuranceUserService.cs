using CGT.Api.DTO;
using CGT.Api.DTO.Insurance.InsuranceUser.Request;
using CGT.PetaPoco.Repositories.Insurance;
using CGT.Entity.CgtInsuranceModel;
using CGT.DDD.Encrpty;
using System;

namespace CGT.Api.Service.Insurance
{
    public class InsuranceUserService : ApiBase<RequestInsuranceUser>
    {
        #region 注入服务
        public InsuranceUserRep insuranceUserRep { get; set; }
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
        /// 执行业务
        /// </summary>
        protected override void ExecuteMethod()
        {
            var userModel = new InsuranceUser()
            {
                UserName = Parameter.UserName,
                UserPwd = Parameter.UserPwd
            };
            var result = insuranceUserRep.Login(userModel);
            Result = new ResponseMessageModel()
            {
                Data = result
            };
        }
    }
}
