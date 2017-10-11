using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise.Request;
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
    /// 企业审核处理流程
    /// </summary>
    public class EnterpriseTempAduitService : ApiBase<RequestEnterpriseTempAduit>
    {
        #region 注入仓储

        public EnterpriseTempoaryRep EpRep { get; set; }

        public ExeclHelper Excel { get; set; }

        #endregion

        private IFormFile file;

        public void SetFileData(IFormFile _file)
        {
            file = _file;
        }

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
            var table = Excel.ExcelImport(file.OpenReadStream());
            var result = table.rows.Remove(table.rows.FirstOrDefault());
            List<EnterpriseTempoary> datalist = new List<EnterpriseTempoary>();
            foreach (var item in table.rows)
            {
                try
                {
                    var EnterpriseTempoaryModel = new EnterpriseTempoary
                    {
                        ID = !String.IsNullOrEmpty(item.columns.ElementAt(0).ColumnValue) ? Convert.ToInt64(item.columns.ElementAt(0).ColumnValue) : 0,
                        EnterpriseName = item.columns.ElementAt(1).ColumnValue,
                        AccountNumber = item.columns.ElementAt(2).ColumnValue,
                        DistributionAccount = item.columns.ElementAt(3).ColumnValue,
                        AccountPeriod = item.columns.ElementAt(4).ColumnValue,
                        TravelServiceAgreementURL = item.columns.ElementAt(5).ColumnValue,
                        CreditAmount = !String.IsNullOrEmpty(item.columns.ElementAt(6).ColumnValue) ? Convert.ToDecimal(item.columns.ElementAt(6).ColumnValue) : 0,
                        RefuseReason = item.columns.ElementAt(7).ColumnValue
                    };
                    datalist.Add(EnterpriseTempoaryModel);
                }
                catch (Exception ex)
                {

                }
            }
            var sql_result = EpRep.UpdateEnterpriseTempoaryStatus(datalist);
            Result.Data = sql_result;
        }
    }
}
