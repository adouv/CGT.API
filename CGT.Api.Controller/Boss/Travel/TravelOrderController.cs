using CGT.Api.DTO;
using CGT.Api.Service.Boss.TravelBill;
using CGT.Api.Service.Boss.TravelOrder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.Api.Controllers.Boss.Travel
{
    /// <summary>
    /// boss差旅订单api
    /// </summary>
    [Produces("application/json")]
    [Route("api/boss/travelorder")]
    [EnableCors("AllowSameDomain")]
    public class TravelOrderController : BaseController
    {
        #region 注入服务
        public DownLoadDistriPaltFormTravelOrderService downLoadDistriPaltFormTravelOrderService { get; set; }
        public GetDistriPaltFormTravelOrderService getDistriPaltFormTravelOrderService { get; set; }
        public GetTravelOrderListService getTravelOrderListService { get; set; }

        public DownloadTravelOrderListService downloadTravelOrderListService { get; set; }

        public ReviewTravelOrderService reviewTravelOrderService { get; set; }

        public GetTravelOrderDataService getTravelOrderDataService { get; set; }
        public GetDownloadEnterpriseOrderListService getDownloadEnterpriseOrderListService { get; set; }
        
        public GetAuditOrderResultService GetAuditOrderResultService { get; set; }

        #endregion
        /// <summary>
        /// 获取差旅订单详情（boss）
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("list"), HttpPost]
        public async Task<ResponseMessageModel> GetTravelOrder([FromBody]RequestModel model)
        {
            getTravelOrderListService.SetData(model);
            return await Task.Run(() => getTravelOrderListService.Execute());
        }
        /// <summary>
        /// 订单下载
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("listDownload"), HttpPost]
        public async Task<ResponseMessageModel> getDownloadEnterpriseOrderList([FromBody]RequestModel model)
        {
            getDownloadEnterpriseOrderListService.SetData(model);
            return await Task.Run(() => getDownloadEnterpriseOrderListService.Execute());
        }
        /// <summary>
        /// 下载差旅订单报表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("downloadlist"), HttpPost]
        public async Task<ResponseMessageModel> DownloadTravelOrder([FromBody]RequestModel model)
        {
            downloadTravelOrderListService.SetData(model);
            return await Task.Run(() => downloadTravelOrderListService.Execute());
        }
        /// <summary>
        /// 查询差旅订单报表数据
        /// </summary>
        /// <param name="model">加密公共实体</param>
        [Route("travelorderdata"), HttpPost]
        public async Task<ResponseMessageModel> GetTravelOrderdata([FromBody]RequestModel model)
        {
            getTravelOrderDataService.SetData(model);
            return await Task.Run(() => getTravelOrderDataService.Execute());
        }
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("revieworder"), HttpPost]
        public async Task<ResponseMessageModel> ReviewTravelOrder([FromBody]RequestModel model)
        {
            reviewTravelOrderService.SetData(model);
            return await Task.Run(() => reviewTravelOrderService.Execute());
        }

        /// <summary>
        /// 获取差旅订单详情(分销平台)
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("DistriPaltFormList"), HttpPost]
        public async Task<ResponseMessageModel> GetDistriPaltFormTravelOrder([FromBody]RequestModel model)
        {
            getDistriPaltFormTravelOrderService.SetData(model);
            return await Task.Run(() => getDistriPaltFormTravelOrderService.Execute());
        }
        /// <summary>
        /// 订单下载（分销平台）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DownLoadDistriPaltFormList"), HttpPost]
        public async Task<ResponseMessageModel> DownLoadDistriPaltFormTravelOrder([FromBody]RequestModel model)
        {
            downLoadDistriPaltFormTravelOrderService.SetData(model);
            return await Task.Run(() => downLoadDistriPaltFormTravelOrderService.Execute());
        } 

        /// <summary>
        /// 人工审核结果查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ArtificialAuditResultQuery"), HttpPost]
        public async Task<ResponseMessageModel> ArtificialAuditResultQuery([FromBody]RequestModel model)
        {
            GetAuditOrderResultService.SetData(model);
            return await Task.Run(() => GetAuditOrderResultService.Execute());
        }

    }
}
