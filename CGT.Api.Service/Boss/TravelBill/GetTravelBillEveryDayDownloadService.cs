using CGT.Api.DTO;
using CGT.Entity.CgtTravelModel;
using CGT.Api.DTO.Boss.TravelBill;
using CGT.DDD.Utils;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Text;
using CGT.DDD.Logger;

namespace CGT.Api.Service.Boss.TravelBill
{
    public class GetTravelBillEveryDayDownloadService : ApiBase<RequestGetTravelEveryBillList>
    {
        #region 注入服务
        public BillEveryDayRep billEveryDayRep { get; set; }
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
                var result = billEveryDayRep.GetBillEveryDayDownload(Parameter.EnterpriseId, Parameter.BillDate, Parameter.Status);
                if (result.Count > 0 && result != null)
                {
                    List<string> titlelist = new List<string>();
                    titlelist.Add("账单编号");
                    titlelist.Add("订单号");
                    titlelist.Add("分销商名称");
                    titlelist.Add("企业名称");
                    titlelist.Add("出资方名称");
                    titlelist.Add("账单日期");
                    titlelist.Add("订单金额");
                    titlelist.Add("还款状态");
                    titlelist.Add("总返现订单量");
                    titlelist.Add("分销商利率");
                    titlelist.Add("分销商利息");   

                    List<string> keylist = new List<string>();
                    keylist.Add("BillId");
                    keylist.Add("OrderId");
                    keylist.Add("PayCenterName");
                    keylist.Add("EnterpriseName");
                    keylist.Add("OwnerName");
                    keylist.Add("BillDateStr");
                    keylist.Add("BillAmount");
                    keylist.Add("StatusStr");
                    keylist.Add("SumTicketNo");
                    keylist.Add("UserInterestRate");
                    keylist.Add("DistributorInterest");  

                    var filebytes = Excel.ExcelExport<BillEveryDay>(result, titlelist, keylist);
                    Result.Data = Convert.ToBase64String(filebytes);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "GetTravelBillEveryDayDownloadService");
                throw new AggregateException(ex.Message);
            }

        }
    }
}
