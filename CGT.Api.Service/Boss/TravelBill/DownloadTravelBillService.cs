using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelBill;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.Api.Service.Boss.TravelBill
{
    /// <summary>
    /// 下载差旅账单
    /// </summary>
    public class DownloadTravelBillService : ApiBase<RequestGetTravelBillList>
    {
        #region 注入服务
        public TravelBillRep travelBillRep { get; set; }

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
            var model = new Bill()
            {
                Status = Convert.ToInt32(Parameter.Status),
                PayCenterCode = this.Parameter.PayCenterCode,
                MerchantType = this.Parameter.MerchantType
            };

            var result = travelBillRep.GetDownloadBillList(model,
                this.Parameter.StartBillDate,
                this.Parameter.EndBillDate).ToList();

            if (result.Any())
            {
                int? MerchantType = result.FirstOrDefault().MerchantType;

                List<string> titlelist = new List<string>();
                titlelist.Add("账单号");
                titlelist.Add("出资方名称");
                titlelist.Add("分销商名称");
                titlelist.Add("账期");
                titlelist.Add("应还款金额");
                titlelist.Add("已还款金额");
                titlelist.Add("还款状态");
                titlelist.Add("逾期天数");
                titlelist.Add("滞纳金金额");
                titlelist.Add("总返现订单量");
                if (MerchantType == 0 || MerchantType == 1 || MerchantType == 3)//分销和代理和财务可以查看
                {
                    titlelist.Add("分销商利率");
                    titlelist.Add("分销商利息");
                    titlelist.Add("退款利息金额");
                }
                if (MerchantType > 1)//金主和财务可以查看
                {
                    titlelist.Add("出资方利率");
                    titlelist.Add("出资方利息");
                }

                List<string> keylist = new List<string>();
                keylist.Add("BillId");
                keylist.Add("OwnerName");
                keylist.Add("PayCenterName");
                keylist.Add("BillDate");
                keylist.Add("BillAmount");
                keylist.Add("AlreadyReimbursement");
                keylist.Add("StatusStr");
                keylist.Add("OverdueDays");
                keylist.Add("OverdueFine");
                keylist.Add("SumTicketNo");
                if (MerchantType == 0 || MerchantType == 1 || MerchantType == 3)//分销和代理和财务可以查看
                {
                    keylist.Add("UserInterestRate");
                    keylist.Add("BillInterest");
                    keylist.Add("InterestRefundAmount");
                }
                if (MerchantType > 1)//金主和财务可以查看
                {
                    keylist.Add("FactoringInterestRate");
                    keylist.Add("FactoringInterest");
                }

                var filebytes = Excel.ExcelExport<TravelBillDownload>(result, titlelist, keylist);
                Result.Data = Convert.ToBase64String(filebytes);
            }
        }
    }
}
