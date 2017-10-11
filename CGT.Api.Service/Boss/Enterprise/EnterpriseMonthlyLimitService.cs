using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System.Linq;

namespace CGT.Api.Service.Boss.Enterprise
{
    /// <summary>
    /// 企业月限额管理
    /// </summary>
    public class EnterpriseMonthlyLimitService : ApiBase<RequestEnterpriseMonthlyLimit>
    {
        public EnterpriseWhiteRep enterpriseWhiteRep { get; set; }
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

        protected override void ExecuteMethod()
        {
            var tempModel = new EnterpriseWhiteList()
            {
                PayCenterCode = this.Parameter.PayCenterCode,
                EnterpriseWhiteListID = this.Parameter.EnterpriseWhiteListID ?? 0,
                MonthStatue = this.Parameter.MonthStatue ?? -1,
                BeginDate = this.Parameter.StartDate,
                EndDate = this.Parameter.EndDate,
                EnterpriseStatue = this.Parameter.EnterpriseStatue ?? -1
            };
            if (!string.IsNullOrEmpty(this.Parameter.IsPage) && this.Parameter.IsPage == "1")//不分页
            {
                var result = enterpriseWhiteRep.GetEnterpriseWhiteAndBackMoneyList(tempModel);
                var CreditAmount = result.Sum(r => r.CreditAmount);
                var AccountBalance = result.Sum(r => r.AccountBalance);
                var EnterpriseCount = result.Count;
                var PayCenterName = result.FirstOrDefault().PayCenterName;
                this.Result.Data = new
                {
                    PayCenterName = PayCenterName,
                    CreditAmount = CreditAmount,
                    AccountBalance = AccountBalance,
                    MonthlyLimitBalance = CreditAmount - AccountBalance,
                    EnterpriseCount = EnterpriseCount
                };
            }
            else
            {
                var result = enterpriseWhiteRep.GetEnterpriseWhiteAndBackMoneyPageList(tempModel, this.Parameter.Pageindex, this.Parameter.Pagesize);
                this.Result.Data = result;
            }
        }
    }
}
