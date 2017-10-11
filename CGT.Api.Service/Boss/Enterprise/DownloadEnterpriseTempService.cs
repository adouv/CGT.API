using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;

namespace CGT.Api.Service.Boss.Enterprise
{
    /// <summary>
    /// 下载审核中企业列表模板
    /// </summary>
    public class DownloadEnterpriseTempService : ApiBase<RequestDownloadEnterpriseTemp> {

        #region 注入仓储

        public EnterpriseTempoaryRep EpRep { get; set; }

        public ExeclHelper Excel { get; set; }

        #endregion

        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json) {
            base.SetData(json);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate() {
            base.Validate();
        }

        protected override void ExecuteMethod() {
            var list = EpRep.GetDistEnterpriseList(Parameter.UserId);

            List<string> titlelist = new List<string>();
            titlelist.Add("企业编号");
            titlelist.Add("企业名称");
            titlelist.Add("共管账号");
            titlelist.Add("分销账号");
            titlelist.Add("账期");
            titlelist.Add("差旅协议地址");
            titlelist.Add("授信金额");
            titlelist.Add("拒绝理由");

            List<string> keylist = new List<string>();
            keylist.Add("ID");
            keylist.Add("EnterpriseName");
            keylist.Add("AccountNumber");
            keylist.Add("DistributionAccount");
            keylist.Add("AccountPeriod");
            keylist.Add("TravelServiceAgreementURL");
            keylist.Add("CreditAmount");
            keylist.Add("RefuseReason");
            var filebytes = Excel.ExcelExport<EnterpriseTempoary>(list, titlelist, keylist);

            Result.Data = Convert.ToBase64String(filebytes);
        }
    }
}
