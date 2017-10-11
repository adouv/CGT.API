using CGT.Entity.CgtTravelModel;
using CGT.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using PetaPoco.NetCore;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    public class EnterpriseStaffRep
    {
        /// <summary>
        /// 批量导入员工白名单列表
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        public int AddStaffList(List<EnterpriseStaff> DataList)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                int index = 0;
                db.BeginTransaction();
                try
                {
                    foreach (var item in DataList)
                    {
                        if (item.EnterpriseId > 0)
                        {
                            db.Insert(item);
                            if (item.ID > 0)
                            {
                                index++;
                            }
                        }
                    }
                    db.CompleteTransaction();
                }
                catch (Exception ex)
                {
                    db.AbortTransaction();
                }

                return index;
            }
        }

        /// <summary>
        /// 查询企业员工白名单列表
        /// </summary>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageSize">页面尺寸</param>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public Page<EnterpriseStaff> QueryStaffList(int PageIndex, int PageSize, EnterpriseStaff model = null)
        {
            string sqlwhere = "";
            if (model != null && model.EnterpriseId > 0)
            {
                sqlwhere += " and EnterpriseId=@0";
            }
            if (model != null && !String.IsNullOrEmpty(model.StaffName))
            {
                sqlwhere += " and StaffName=@1";
            }
            if (model != null && !String.IsNullOrEmpty(model.PayCenterCode))
            {
                sqlwhere += " and PayCenterCode=@2";
            }
            using (var db = CgtTravelDB.GetInstance())
            {
                var page = db.Page<EnterpriseStaff>(PageIndex, PageSize, "select a.*,b.EnterpriseName,b.PayCenterCode from dbo.EnterpriseStaff a INNER JOIN dbo.EnterpriseWhiteList b ON a.EnterpriseId=b.EnterpriseWhiteListID where 1=1 " + sqlwhere + " ORDER BY ID DESC ", model?.EnterpriseId, model?.StaffName, model?.PayCenterCode);
                return page;
            }
        }

        /// <summary>
        /// 编辑更新员工
        /// </summary>
        /// <param name="model">修改员工对象</param>
        /// <returns></returns>
        public int UpdateStaff(EnterpriseStaff model)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                var StaffModel = db.Single<EnterpriseStaff>(model.ID);

                StaffModel.StaffName = model.StaffName;
                StaffModel.IdentificationNumber = model.IdentificationNumber;
                StaffModel.DocumentType = model.DocumentType;
                StaffModel.PhoneNumber = model.PhoneNumber;
                StaffModel.SubsidiaryDepartment = model.SubsidiaryDepartment;
                StaffModel.JobPosition = model.JobPosition;
                StaffModel.IsJob = model.IsJob;

                return db.Update(StaffModel);
            }
        }

        /// <summary>
        /// 查询企业员工白名单列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public List<EnterpriseStaff> QueryStaffList(EnterpriseStaff model = null)
        {
            List<EnterpriseStaff> list = new List<EnterpriseStaff>();
           string sqlwhere = "";
            if (model != null && model.EnterpriseId > 0)
            {
                sqlwhere += " and EnterpriseId=@0";
            }
            if (model != null && !String.IsNullOrEmpty(model.StaffName))
            {
                sqlwhere += " and StaffName=@1";
            }
            if (model != null && !String.IsNullOrEmpty(model.PayCenterCode))
            {
                sqlwhere += " and PayCenterCode=@2";
            }
            using (var db = CgtTravelDB.GetInstance())
            {
                list =db.Fetch<EnterpriseStaff>("select a.*,b.EnterpriseName,b.PayCenterCode from dbo.EnterpriseStaff a INNER JOIN dbo.EnterpriseWhiteList b ON a.EnterpriseId=b.EnterpriseWhiteListID where 1=1 " + sqlwhere + " ORDER BY ID DESC ", model?.EnterpriseId, model?.StaffName, model?.PayCenterCode);
                
                return list;
            }
        }
    }
}
