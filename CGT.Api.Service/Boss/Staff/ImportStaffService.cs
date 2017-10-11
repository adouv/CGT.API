using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Staff;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.Api.Service.Boss.Staff
{
    /// <summary>
    /// 上传员工列表
    /// </summary>
    public class ImportStaffService : ApiBase<RequestImportStaff>
    {
        #region 注入仓储

        public EnterpriseStaffRep EpRep { get; set; }

        public ExeclHelper Excel { get; set; }

        #endregion

        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }

        private IFormFile file;

        /// <summary>
        /// 当前业务文件对象
        /// </summary>
        /// <param name="_file"></param>
        public void SetFileData(IFormFile _file)
        {
            file = _file;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        protected override void ExecuteMethod()
        {
            //导出Excel数据
            var table = Excel.ExcelImport(file.OpenReadStream());
            //去除表头
            table.rows.Remove(table.rows.FirstOrDefault());
            //组织批量数据库实体
            List<EnterpriseStaff> DataList = new List<EnterpriseStaff>();
            foreach (var item in table.rows)
            {
                byte i = 1;
                byte p = 0;
                if (!string.IsNullOrWhiteSpace(item.columns.ElementAt(0).ColumnValue))
                {
                    var EnterpriseStaffModel = new EnterpriseStaff
                    {
                        EnterpriseId = Parameter.EnterpriseId,
                        StaffName = Convert(item.columns.ElementAt(0).ColumnValue),
                        PhoneNumber = Convert(item.columns.ElementAt(1).ColumnValue),
                        IdentificationNumber = Convert(item.columns.ElementAt(2).ColumnValue),
                        IsJob = TranIsJob(item.columns.ElementAt(3).ColumnValue.Trim()),
                        SubsidiaryDepartment = Convert(item.columns.ElementAt(4).ColumnValue),
                        JobPosition = Convert(item.columns.ElementAt(5).ColumnValue),
                        DocumentType = 1,
                        CreateTime = DateTime.Now,
                        ModifyTime = DateTime.Now,
                        Modifier = Parameter.UserId
                    };
                    DataList.Add(EnterpriseStaffModel);
                }
            }
            //数据库批量插入
            this.Result.Data = EpRep.AddStaffList(DataList);
        }

        private string Convert(string str) => (String.IsNullOrEmpty(str) ? null : str.Trim());

        private byte? TranIsJob(string str) => new Func<Nullable<Byte>>(() =>
          {
              switch (str)
              {
                  case "是":
                      return 1;
                  case "否":
                      return 0;
                  default:
                      return null;
              }
          })();
    }

}
