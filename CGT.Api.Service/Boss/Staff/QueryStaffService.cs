using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Staff;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;

namespace CGT.Api.Service.Boss.Staff
{
    /// <summary>
    /// 查询企业员工
    /// </summary>
    public class QueryStaffService : ApiBase<RequestQueryStaff>
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

        /// <summary>
        /// 接口执行方法
        /// </summary>
        protected override void ExecuteMethod()
        {
            EnterpriseStaff StaffModel = null;
            ///当前请求企业ID大于0的加入企业ID
            if (this.Parameter.EnterpriseId > 0)
            {
                if (StaffModel == null) { StaffModel = new EnterpriseStaff(); }
                StaffModel.EnterpriseId = this.Parameter.EnterpriseId != null ? (long)this.Parameter.EnterpriseId : 0;
            }
            //当前员工姓名不为空加入员工姓名为请求条件
            if (!String.IsNullOrEmpty(this.Parameter.StaffName))
            {
                if (StaffModel == null) { StaffModel = new EnterpriseStaff(); }
                StaffModel.StaffName = this.Parameter.StaffName;
            }
            if (!String.IsNullOrEmpty(this.Parameter.PayCenterCode))
            {
                if (StaffModel == null) { StaffModel = new EnterpriseStaff(); }
                StaffModel.PayCenterCode = this.Parameter.PayCenterCode;
            }
            this.Result.Data = EpRep.QueryStaffList(Parameter.PageIndex, Parameter.PageSize, StaffModel);
        }
    }
}
