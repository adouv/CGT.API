using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.Entity.CgtModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.UserCenter.Service;
using Org.BouncyCastle.Utilities.IO;
using System;
using CGT.DDD.Utils;
using CGT.Api.Service.Common;
using CGT.DDD.Encrpty;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 代理注册
    /// </summary>
    public class RegisteredAgentService : ApiBase<RequestRegisteredAgentModel>
    {
        #region 注入服务
        public InterfaceAccountRep interfaceRep { get; set; }
        public RegisteredMerchantProcessor registeredMerchantProcessor { get; set; }
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
            var datainterfaceAccount = interfaceRep.GetInterfaceAccount(new InterfaceAccount() { ReapayMerchantNo = this.Parameter.ReapayMerchantNo });
            if (datainterfaceAccount != null)
            {
                throw new Exception("账号已存在,注册失败！");
            }
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod()
        {

            Random rd = new Random();
            var MerchantCode = ChineseSpellHelp.GetChineseSpell(this.Parameter.MerchantName) + rd.Next(100, 999);
            var MerchantPwd = Encrpty.MD5Pwd(this.Parameter.MerchantPwd);
            registeredMerchantProcessor.InitData(this.Parameter.ReapayMerchantNo, this.Parameter.MerchantPwd,
                MerchantCode, this.Parameter.MerchantName, this.Parameter.Contact, "",
                "", this.Parameter.ReapayMerchantNo, this.Parameter.ReapalMerchantId, this.Parameter.Phone, "", this.Parameter.ReapayMerchantNo, this.Parameter.ReapalMerchantPwd.Trim());
            var result = registeredMerchantProcessor.Execute();
            if (!result.Success)
            {
                throw new Exception(result.Message);
            }
            InterfaceAccount _InterfaceAccount = new InterfaceAccount()
            {
                Contact = this.Parameter.Contact,
                CreateTime = DateTime.Now,
                CreateUserID = 0,
                MerchantCode = MerchantCode,
                MerchantName = this.Parameter.MerchantName,
                MerchantPwd = MerchantPwd,
                Phone = this.Parameter.Phone,
                ReapalMerchantId = this.Parameter.ReapalMerchantId,
                ReapayMerchantNo = this.Parameter.ReapayMerchantNo,
                Status = 0,
                UpdateTime = DateTime.Now,
                UserKey = Guid.NewGuid().ToString().Replace("-", ""),
                CertAddress = "",
                UpdateUserID=0,
                IsCheckPrice=0
            };
            interfaceAccountRep.Insert(_InterfaceAccount);

            //生成证书
            var interfaceAccountmodel = ServiceCommon.GenerateUserCer(_InterfaceAccount);
            _InterfaceAccount.CertAddress = interfaceAccountmodel.CertAddress;
            _InterfaceAccount.CertPassword = interfaceAccountmodel.CertPassword;
            int i = interfaceAccountRep.Update(_InterfaceAccount);
            if (i <= 0)
            {
                throw new System.Exception("更新数据库失败");
            }
        }
    }
}
