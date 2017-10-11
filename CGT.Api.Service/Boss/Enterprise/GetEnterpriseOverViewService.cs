using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;

using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.Enterprise
{
    public class GetEnterpriseOverViewService : ApiBase<RequestGetEnterpriseOverView>
    {
        #region 注入服务
        public EnterpriseWhiteRep enterpriseWhiteRep { get; set; }
        public BillRep billRep { get; set; }
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
            decimal RemainingCreditLimit = 0m;//剩余售信额度
            decimal CreditLimit = 0m; //总售信额度
            decimal OverdueAmout = 0m;//账单逾期总金额

            if (this.Parameter.PayCenterCode==null || this.Parameter.PayCenterCode=="")
            {
                CreditLimit = enterpriseWhiteRep.GetEnterpriseWhiteOverView(out RemainingCreditLimit);
                OverdueAmout = billRep.GetAllGraceAndOverdue();
            }
            else
            {
                CreditLimit = enterpriseWhiteRep.GetEnterpriseWhiteOverView(this.Parameter.PayCenterCode, out RemainingCreditLimit);
                OverdueAmout = billRep.GetAllGraceAndOverdue(this.Parameter.PayCenterCode);
            }

            this.Result.Data = new ResponseGetEnterpriseOverView()
            {
                RemainingCreditLimit = RemainingCreditLimit, //授信余额
                CreditLimit = CreditLimit,//授信总额
                OverdueAmout = OverdueAmout, // 逾期总额
                OutstandingAmount=CreditLimit-RemainingCreditLimit 
            };
        }
    }
}
