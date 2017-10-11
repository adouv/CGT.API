using CGT.Api.DTO;
using CGT.Api.Service.Boss.Refunds;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace CGT.Api.Controllers.Boss.Refunds
{
    /// <summary>
    /// 采购通国际票退票
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/Refunds")]
    [EnableCors("AllowSameDomain")]
    public class InterRefundController : BaseController
    {
        public GetInterRefundService getInterRefundService { get; set; }

        public GetInterRefundDetailService getInterRefundDetailService { get; set; }

        public ModtifyInterRefundAffairStatusService modtifyInterRefundAffairStatusService { get; set; }

        public DownloadRefundListService downloadRefundListService { get; set; }

       public SubmitRefundService submitRefundService { get; set; }
        /// <summary>
        /// 国际票退票列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("InterRefundList"), HttpPost]
        public ResponseMessageModel GetInterRefundList([FromBody]RequestModel model)
        {
            getInterRefundService.SetData(model);
            return getInterRefundService.Execute();
        }
        /// <summary>
        /// 退票祥单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("InterRefundDetail"), HttpPost]
        public ResponseMessageModel GetInterRefundDetail([FromBody]RequestModel model)
        {
            getInterRefundDetailService.SetData(model);
            return getInterRefundDetailService.Execute();
        }
        /// <summary>
        /// 修改锁单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ModtifyInterRefundAffairStatus"), HttpPost]
        public ResponseMessageModel ModtifyInterRefundAffairStatus([FromBody]RequestModel model)
        {
            modtifyInterRefundAffairStatusService.SetData(model);
            return modtifyInterRefundAffairStatusService.Execute();
        }
        /// <summary>
        /// 提交退票
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("SubmitRefund"), HttpPost]
        public ResponseMessageModel SubmitRefund([FromBody]RequestModel model)
        {
            submitRefundService.SetData(model);
            return submitRefundService.Execute();
        }
        /// <summary>
        /// 下载退票列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DownloadRefundList"), HttpPost]
        public ResponseMessageModel DownloadRefundList([FromBody]RequestModel model)
        {
            downloadRefundListService.SetData(model);
            return downloadRefundListService.Execute();
        }
    }
}
