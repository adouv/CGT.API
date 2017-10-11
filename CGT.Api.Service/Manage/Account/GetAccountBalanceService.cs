using CGT.Api.DTO;
using CGT.Api.DTO.Manage.Account;
using CGT.Api.DTO.Manage.Account.Response;
using CGT.Reapal.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Manage.Account
{
    public class GetAccountBalanceService : ApiBase<RequestGetAccountBalance>
    {

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

            var MemberAccount = new EnterprisAccoutProcessor(this.Parameter.ReapalMerchantId);
            var MemberResult = MemberAccount.Execute();
            if (MemberResult != null)
            {
                this.Result.Data = new ResponseGetAccountBalance()
                {
                    Balance =Convert.ToDecimal(MemberResult.Result.balance *0.01)
                };
            }
            
        }
    }
}
