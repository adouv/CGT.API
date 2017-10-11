using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.Api.Service.Boss.Enterprise
{
    /// <summary>
    /// 导入分销企业列表
    /// </summary>
    public class ImportEnterpriseListService : ApiBase<RequestImportEnterpriseList>
    {
        #region 注入仓储

        public EnterpriseTempoaryRep EpRep { get; set; }

        public ExeclHelper Excel { get; set; }

        #endregion

        private IFormFile file;

        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }

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
            var table = Excel.ExcelImport(file.OpenReadStream());
            table.rows.Remove(table.rows.FirstOrDefault());
            List<EnterpriseTempoary> DataList = new List<EnterpriseTempoary>();
            foreach (var item in table.rows)
            {
                try
                {
                    var EnterpriseTempoaryModel = new EnterpriseTempoary
                    {
                        EnterpriseName = item.columns.ElementAt(0).ColumnValue.Trim(),
                        AccountNumber = item.columns.ElementAt(1).ColumnValue.Trim(),
                        CreditAmount = Convert.ToDecimal(item.columns.ElementAt(2).ColumnValue),
                        DistributionAccount = item.columns.ElementAt(3).ColumnValue.Trim(),
                        AccountPeriod = item.columns.ElementAt(4).ColumnValue.Trim()
                    };

                    DataList.Add(EnterpriseTempoaryModel);
                }
                catch (Exception ex)
                {

                }
            }
            var result = EpRep.QuestSaveList(DataList, this.Parameter.UserId);
            this.Result.Data = result;
        }
    }
}
