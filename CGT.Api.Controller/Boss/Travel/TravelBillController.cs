using CGT.Api.DTO;
using CGT.Api.Service.Boss.TravelBill;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CGT.Api.Controllers.Boss.Travel
{
    /// <summary>
    /// 差旅账单API
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/TravelBill")]
    [EnableCors("AllowSameDomain")]
    public class TravelBillController : Controller
    {
        #region 注入服务
        public GetTravelBillService billService { get; set; }

        public GetTravelBillDownloadService billDownloadService { get; set; }

        public GetTravelEveryBillService everyBillService { get; set; }
        public GetTravelBillEveryDayDownloadService everyBillDownloadService { get; set; }

        public RepayTravelBillByGoldService repayTravelBillByGoldService { get; set; }

        #endregion

        /// <summary>
        /// 账单列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("BillList"), HttpPost]
        public ResponseMessageModel GetTravelBillList([FromBody]RequestModel model)
        {
            billService.SetData(model);
            return billService.Execute();
        }

        /// <summary>
        /// 下载账单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DownloadBill"), HttpPost]
        public ResponseMessageModel DownloadTravelBill([FromBody]RequestModel model)
        {
            billDownloadService.SetData(model);
            return billDownloadService.Execute();
        }

        /// <summary>
        /// 日账单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("EveryBill"), HttpPost]
        public ResponseMessageModel GetEveryTravelBillList([FromBody]RequestModel model)
        {
            everyBillService.SetData(model);
            return everyBillService.Execute();
        }
        /// <summary>
        /// 日账单详情下载
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DownloadBillEveryDay"), HttpPost]
        public ResponseMessageModel DownloadTravelBillEveryDay([FromBody]RequestModel model)
        {
            everyBillDownloadService.SetData(model);
            return everyBillDownloadService.Execute();
        }
        /// <summary>
        /// 金主确认还款
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("RepayTravelBillByGold"), HttpPost]
        public ResponseMessageModel RepayTravelBillByGold([FromBody]RequestModel model) {
            repayTravelBillByGoldService.SetData(model);
            return repayTravelBillByGoldService.Execute();
        }

    }
}
