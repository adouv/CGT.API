using CGT.PetaPoco.Repositories.Insurance;
using System;

namespace CGT.Base.Service.Insurance
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateMonthlylimitCountService : ServiceBase
    {
        public InsuranceUserRep insuranceUserRep { get; set; }

        protected override void ExecuteMethod()
        {
            int i = insuranceUserRep.UpdateRemainingCount();
            if (i < 1)
            {
                this.Result.IsSuccess = false;
                this.Result.Message = "更新数据库失败";
            }
        }
    }
}
