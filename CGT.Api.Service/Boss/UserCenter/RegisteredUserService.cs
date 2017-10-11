using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.DDD.Encrpty;
using CGT.Entity.CgtModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.UserCenter.Service;
using MongoDB.Driver.Core.Authentication;
using Org.BouncyCastle.Utilities.IO;
using System;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 分销注册
    /// </summary>
    public class RegisteredUserService : ApiBase<RequestRegisteredUserModel>
    {
        #region 注入服务
        public UserAccountRep userAccountRep { get; set; }
        public InterfaceAccountRep interfaceRep { get; set; }
        public UpdateMerchantProcessor updateMerchantProcessor { get; set; }
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
            var datauserAccount = userAccountRep.GetUserAccount(new UserAccount()
            {
                UserName = this.Parameter.ReapayMerchantNo
            });
            if (datauserAccount != null)
            {
                throw new System.Exception("账号已存在，注册失败！");
            }
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod()
        {
            var MerchantPwd = Encrpty.MD5Pwd(this.Parameter.MerchantPwd);
            var user = new UserAccount()
            {
                UserName = this.Parameter.ReapayMerchantNo,
                Email = this.Parameter.ReapayMerchantNo,
                UserPwd = MerchantPwd,
                RealName = this.Parameter.Contact,
                Phone = this.Parameter.Phone,
                Ip = "127.0.0.1",
                Status = 2,
                ReapalMerchantId = this.Parameter.ReapalMerchantId,
                CreateTime = DateTime.Now,
                UserType = 3,
                PartnerCode = "00",
                Vip = 0,
                IsOnVip = 0,
                TicketDelayEmail = this.Parameter.ReapayMerchantNo,
                MerchantCode = this.Parameter.MerchantCode,
                UserCompanyName = this.Parameter.MerchantName,
                IdNumber = "",
                BankCardNumber = "",
                LCCReceivesEmail="",
                BillLateFee=0,
                GraceCount = 0,
                OverdueCount = 0
            };
            userAccountRep.Insert(user);
            user.PayCenterCode = this.Parameter.MerchantCode + user.UserId.ToString();
            var interfaceUser = interfaceAccountRep.GetInterfaceAccount(new InterfaceAccount() { MerchantCode = this.Parameter.MerchantCode });
            updateMerchantProcessor.InitData(interfaceUser.ReapayMerchantNo, interfaceUser.MerchantCode,
                this.Parameter.ReapayMerchantNo,this.Parameter.ReapalMerchantId,this.Parameter.MerchantName,user.PayCenterCode);
            var result = updateMerchantProcessor.Execute();
            if (!result.Success)
            {
                throw new Exception(result.Message);
            }
            user.Status = 0;
            int returnVal = userAccountRep.Update(user);
            if (returnVal <= 0)
            {
                throw new Exception("更新数据库失败");
            }
        }
    }
}
