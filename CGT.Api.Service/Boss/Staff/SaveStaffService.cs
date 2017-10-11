using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Staff;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;

namespace CGT.Api.Service.Boss.Staff
{
    /// <summary>
    /// 修改单个用户信息
    /// </summary>
    public class SaveStaffService : ApiBase<RequestSaveStaff>
    {
        #region 注入仓储

        public EnterpriseStaffRep EpRep { get; set; }

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

        protected override void ExecuteMethod()
        {
            EnterpriseStaff StaffModel = new EnterpriseStaff
            {
                ID = Parameter.ID,
                StaffName = Parameter.StaffName,
                IdentificationNumber = Parameter.IdentificationNumber,
                DocumentType = Parameter.DocumentType,
                PhoneNumber = Parameter.PhoneNumber,
                SubsidiaryDepartment = Parameter.SubsidiaryDepartment,
                JobPosition = Parameter.JobPosition,
                IsJob = Parameter.IsJob
            };
            Result.Data = EpRep.UpdateStaff(StaffModel);
        }
    }
}