using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;

namespace CGT.Base.Service.Factoring
{
    public class OverDueMonthlyBillService : ServiceBase
    {
        public BillRep BillRepositories { get; set; }

        public UserAccountRep UserAccountRepositories { get; set; }

        /// <summary>
        /// 计算逾期宽限期利息
        /// </summary>
        protected override void ExecuteMethod()
        {
            var list = BillRepositories.GetAllOutstandingBill();
            var now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            foreach (var item in list)
            {
                //当前属于宽限期
                if (item.GraceDay != null && item.GraceDay > 0 && now > item.BillDate && now <= item.GraceDate)
                {
                    var timediff = now - item.BillDate;
                    //去更新分销宽限期次数
                    if (timediff.Days == 1)
                    {
                        //计算分销商宽限期次数
                        UserAccountRepositories.UpdateUserAccountGraceCount(item.PayCenterCode);
                        //修改账单状态
                        BillRepositories.UpdateBillType(1, item.BillId);
                    }
                    //计算分销商宽限期利息
                    BillRepositories.UpdateGraceAmout((item.BillAmount - item.AlreadyReimbursement) * item.GraceBate* timediff.Days, item.BillId);
                }
                //当前属于逾期
                else if (item.GraceDate != null && now > item.GraceDate)
                {
                    var timediff = now - Convert.ToDateTime(item.GraceDate);
                    //去更新分销逾期次数
                    if (timediff.Days == 1)
                    {
                        //计算分销商逾期次数
                        UserAccountRepositories.UpdateUserAccountOverdueCount(item.PayCenterCode);
                        //修改账单状态
                        BillRepositories.UpdateBillType(2, item.BillId);
                        //冻结对应未还企业
                        BillRepositories.FrozenEnterpriseList(item.BillId);
                    }
                    //计算分销商逾期利息
                    BillRepositories.UpdateOverdueAmout((item.BillAmount - item.AlreadyReimbursement) * item.OverdueBate * timediff.Days, item.BillId);
                }
                //当前属于正常账期
                else
                {

                }
            }
        }
    }
}
