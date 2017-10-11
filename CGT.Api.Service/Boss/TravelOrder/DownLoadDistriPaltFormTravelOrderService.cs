using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelOrder;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;

namespace CGT.Api.Service.Boss.TravelOrder {
    /// <summary>
    /// 差旅订单下载
    /// </summary>
    public class DownLoadDistriPaltFormTravelOrderService : ApiBase<RequestDownloadTravelOrderList> {
        #region 注入服务
        public EnterpriseOrderRep enterpriseOrderRep { get; set; }
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

        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            var model = new FactoringOrderModel() {
                BillId = Parameter.BillId,
                BeginBillDate = Parameter.StartBillDate,
                EndBillDate = Parameter.EndBillDate,
                EnterpriseId = Parameter.EnterpriseId,
                EnterpriseName = Parameter.EnterpriseName,
                MerchantCode = Parameter.MerchantCode,
                BackStatus = Parameter.BackStatus,
                OrderTravelBatchId=this.Parameter.OrderTravelBatchId,
                DataSource = this.Parameter.DataSouce,
                PayCenterCode = this.Parameter.PayCenterCode
            };
            var list = enterpriseOrderRep.GetEnterpriseOrderDownload(model);

            List<string> titlelist = new List<string>();
            titlelist.Add("批次号");
            titlelist.Add("账单号");
            titlelist.Add("订单号");
            titlelist.Add("出资方名称");
            titlelist.Add("分销商名称");
            titlelist.Add("企业名称");
            titlelist.Add("账期");
            titlelist.Add("应还款金额");
            titlelist.Add("已还款金额");
            titlelist.Add("已还款日期");
            titlelist.Add("还款状态");
            titlelist.Add("逾期天数");
            titlelist.Add("滞纳金金额");
            titlelist.Add("出票日期");
            titlelist.Add("返现时间");
            titlelist.Add("PNR");
            titlelist.Add("航班号");
            titlelist.Add("乘机人");
            titlelist.Add("起飞日期");
            titlelist.Add("分销商利率");
            titlelist.Add("分销商利息");
            titlelist.Add("应退款利息金额");
            titlelist.Add("返现状态");
           // titlelist.Add("出资方利率");
           // titlelist.Add("出资方利息");
            #region new

            //titlelist.Add("宽限期天数");
            //titlelist.Add("宽限期开始时间");
            //titlelist.Add("宽限期结束时间");
            //titlelist.Add("宽限期利率");
            //titlelist.Add("宽限期利息金额");
            //titlelist.Add("逾期天数");
            //titlelist.Add("逾期开始日期");
            //titlelist.Add("逾期结束日期");
            //titlelist.Add("逾期利率");
            //titlelist.Add("逾期利息金额");

           // titlelist.Add("保证险金额");
            #endregion

            List<string> keylist = new List<string>();
            keylist.Add("OrderTravelBatchId");
            keylist.Add("BillId");
            keylist.Add("OrderId");
            keylist.Add("AdvancesName");
            keylist.Add("PayCenterName");
            keylist.Add("EnterpriseName");
            keylist.Add("BillDate");
            keylist.Add("TicketAmount");
            keylist.Add("RepaymentTicketAmount");
            keylist.Add("RepaymentDate");
            keylist.Add("RepaymentStatusName");
            keylist.Add("OverdueDays");
            keylist.Add("OverdueFine");
            keylist.Add("TicketTime");
            keylist.Add("BackTime");
            keylist.Add("Pnr");
            keylist.Add("FlightNo");
            keylist.Add("PassengerName");
            keylist.Add("DepartureTime");
            keylist.Add("UserInterestRate");
            keylist.Add("UserInterest");
            keylist.Add("RefundAmount");
            keylist.Add("BackStatusName");
            // keylist.Add("FactoringInterestRate");
            // keylist.Add("FactoringInterestAmount");

            //keylist.Add("GraceDayCount");
            //keylist.Add("StartGraceDay");
            //keylist.Add("EndGraceDay");
            //keylist.Add("GraceBate");
            //keylist.Add("GraceAmout");
            //keylist.Add("OverDayCount");
            //keylist.Add("StartOverDay");
            //keylist.Add("EndOverDay");
            //keylist.Add("OverdueBate");
            //keylist.Add("OverdueAmout");

            // keylist.Add("InsuredAmount");

            //todo
            IList<KeyValuePair<string, string>> headerNameList = new List<KeyValuePair<string, string>>();
            for (var index = 0; index < keylist.Count; index++) {
                headerNameList.Add(new KeyValuePair<string, string>(key: keylist[index], value: titlelist[index]));
            }

            var filebytes = Excel.ExcelExport<FactoringOrderDownload>(list, titlelist, keylist);
            Result.Data = Convert.ToBase64String(filebytes);
        }
    }
}
