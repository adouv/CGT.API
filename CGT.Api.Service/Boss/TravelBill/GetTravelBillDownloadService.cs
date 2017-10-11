using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelBill;
using CGT.DDD.Logger;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;

namespace CGT.Api.Service.Boss.TravelBill
{
    public class GetTravelBillDownloadService : ApiBase<RequestGetTravelBillList>
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
        /// 执行业务
        /// </summary>
        protected override void ExecuteMethod()
        {
            try
            {
                var result = travelBillRep.GetTravelBillListDownload(
                              Parameter.PayCenterCode,
                              Parameter.Status,
                              Parameter.BillType,
                              Parameter.StartBillDate,
                              Parameter.EndBillDate);

                if (result.Count > 0 && result != null)
                {
                    foreach (var item in result)
                    {
                        item.OverdueAmout = Convert.ToDecimal(0.00);
                    }
                    List<string> titlelist = new List<string>();
                    titlelist.Add("账单编号");
                    titlelist.Add("分销商名称");
                    titlelist.Add("出资方名称");
                    titlelist.Add("账单日期");
                    titlelist.Add("应还款金额");
                    titlelist.Add("已还金额");
                    titlelist.Add("还款状态");
                    titlelist.Add("账单类型");
                    titlelist.Add("总返现订单量");
                    
                    titlelist.Add("分销商利率");
                    titlelist.Add("分销商利息");

                    titlelist.Add("宽限期开始日期");
                    titlelist.Add("宽限期结束日期");
                    titlelist.Add("宽限期利息金额");
                    titlelist.Add("宽限期利率");
                    titlelist.Add("宽限天数");

                    titlelist.Add("逾期开始日期");
                    titlelist.Add("逾期结束日期");
                    titlelist.Add("逾期金额");
                    titlelist.Add("逾期利率");
                    titlelist.Add("逾期天数");

                    List<string> keylist = new List<string>();
                    keylist.Add("BillId");
                    keylist.Add("PayCenterName");
                    keylist.Add("OwnerName");
                    keylist.Add("BillDateStr");
                    keylist.Add("BillAmount");
                    keylist.Add("AlreadyReimbursement");
                    keylist.Add("StatusStr");
                    keylist.Add("BillTypeStr");
                    keylist.Add("SumTicketNo");
                    
                    keylist.Add("UserInterestRate");
                    keylist.Add("DistributorInterest");

                    keylist.Add("GraceStartDate");
                    keylist.Add("GraceEndDate");
                    keylist.Add("GraceAmout");
                    keylist.Add("GraceBate");
                    keylist.Add("GraceDay");

                    keylist.Add("OverdueStartDate");
                    keylist.Add("OverdueEndDate");
                    keylist.Add("OverdueAmout");
                    keylist.Add("OverdueBate");
                    keylist.Add("OverdueDay");

                    var filebytes = Excel.ExcelExport<Bill>(result, titlelist, keylist);
                    Result.Data = Convert.ToBase64String(filebytes);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "GetTravelBillDownloadServiceError");
                throw new AggregateException(ex.Message);
            }

        }

    }
}
