using CGT.Api.DTO;
using CGT.Api.Service.Boss.Enterprise;
using CGT.DDD.Config;
using CGT.DDD.Utils.File;
using CGT.DDD.Utils.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CGT.Api.Controllers.Boss.Travel
{
    /// <summary>
    /// BOSS差旅企业白名单API
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/TravelEnterprise")]
    [EnableCors("AllowSameDomain")]
    public class TravelEnterpriseController : BaseController
    {
        #region 注入服务

        public GetEnterpriseListService GetEnterpriseListSer { get; set; }

        public GetEnterpriseWhitePageListService GetEnterpriseWhitePageListSer { get; set; }

        public ImportEnterpriseListService ImportEnterpriseListSer { get; set; }

        public GetEnterpriseWhiteListService getEnterpriseWhiteListService { get; set; }

        public ImportProtocolService ImportProtocolSer { get; set; }

        public DownloadEnterpriseTempService DownloadEnterpriseTempSer { get; set; }

        public EnterpriseTempAduitService EnterpriseTempAduitSer { get; set; }

        public DistributionEnterpriseService DistributionEnterpriseSer { get; set; }

        public EnterpriseMonthlyLimitService EnterpriseMonthlyLimitSer { get; set; }


        public EditEnterpriseMonthStatusService EditEnterpriseMonthStatusSer { get; set; }

        public GetEnterpriseOverViewService GetEnterpriseOverViewSer { get; set; }

        public UpdateEnterpriseCreditAmountService UpdateEnterpriseCreditAmountSer { get; set; }

        #endregion

        /// <summary>
        /// 获取企业白名单
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("EPList"), HttpPost]
        public ResponseMessageModel GetEnterpriseTempoaryList([FromBody]RequestModel model)
        {
            GetEnterpriseListSer.SetData(model);
            return GetEnterpriseListSer.Execute();
        }

        /// <summary>
        /// 分销上传企业临时列表
        /// </summary>
        /// <returns></returns>
        [Route("UploadEPList"), HttpPost]
        public ResponseMessageModel ImportEnterpriseTempoaryList(IFormFile File)
        {
            ImportEnterpriseListSer.SetFileData(File);
            RequestModel model = JsonConvert.DeserializeObject<RequestModel>(MyHttpContext.Current.Request.Form["formData"]);
            ImportEnterpriseListSer.SetData(model);
            return ImportEnterpriseListSer.Execute();
        }

        /// <summary>
        /// 分销企业模板下载页面
        /// </summary>
        /// <returns></returns>
        [Route("DownloadEPExcel"), HttpPost]
        public ResponseMessageModel DownloadEnterpriseExcel()
        {
            try
            {
                string fileconfig = JsonConfig.JsonRead("EnterpriseTemporayTemplate", "CGTExcel");
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), fileconfig);
                string result = null;
                if (System.IO.File.Exists(filepath))
                {
                    var stream = FileUtily.FileToStream(filepath);
                    var ExcelBytes = FileUtily.StreamToBytes(stream);
                    result = Convert.ToBase64String(ExcelBytes);

                    return new ResponseMessageModel
                    {
                        Data = result,
                        Message = "成功",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new ResponseMessageModel
                    {
                        Data = "文件不存在路径异常",
                        Message = "成功",
                        IsSuccess = true
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessageModel
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    ErrorCode = "9999"
                };
            }
        }

        /// <summary>
        /// 获取企业白名单
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("EnterpriseWhiteList"), HttpPost]
        public ResponseMessageModel GetEnterpriseWhiteList([FromBody]RequestModel model)
        {
            getEnterpriseWhiteListService.SetData(model);
            return getEnterpriseWhiteListService.Execute();
        }
        /// <summary>
        /// 获取企业白名单(分页)
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("EnterpriseWhitePageList"), HttpPost]
        public ResponseMessageModel GetEnterpriseWhitePageList([FromBody]RequestModel model) {
            GetEnterpriseWhitePageListSer.SetData(model);
            return GetEnterpriseWhitePageListSer.Execute();
        }
        /// <summary>
        /// 导出差旅协议
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        [Route("ImportProtocol"), HttpPost]
        public ResponseMessageModel ImportProtocol(IFormFile File)
        {
            RequestModel model = JsonConvert.DeserializeObject<RequestModel>(MyHttpContext.Current.Request.Form["formData"]);
            ImportProtocolSer.SetData(model);
            ImportProtocolSer.SetFileData(File);
            return ImportProtocolSer.Execute();
        }

        /// <summary>
        /// 下载已传协议未审核分销数据模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DownloadEnterpriseTemp"), HttpPost]
        public ResponseMessageModel DownloadEnterpriseTemp([FromBody]RequestModel model)
        {
            DownloadEnterpriseTempSer.SetData(model);
            return DownloadEnterpriseTempSer.Execute();
        }

        /// <summary>
        /// 企业审核
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        [Route("EnterpriseTempAduit"), HttpPost]
        public async Task<ResponseMessageModel> EnterpriseTempAduit(IFormFile File)
        {
            RequestModel model = JsonConvert.DeserializeObject<RequestModel>(MyHttpContext.Current.Request.Form["formData"]);
            EnterpriseTempAduitSer.SetFileData(File);
            EnterpriseTempAduitSer.SetData(model);
            return await Task.Run(()=>EnterpriseTempAduitSer.Execute());
        }

        /// <summary>
        /// 分销企业2级联动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DistributionEnterprise"), HttpPost]
        public async Task<ResponseMessageModel> DistributionEnterprise([FromBody]RequestModel model)
        {
            DistributionEnterpriseSer.SetData(model);
            return await Task.Run(()=>DistributionEnterpriseSer.Execute());
        }
        /// <summary>
        /// 企业月限额管理(分页)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("GetEnterpriseMonthlyLimit"), HttpPost]
        public async Task<ResponseMessageModel> GetEnterpriseMonthlyLimit([FromBody]RequestModel model)
        {
            EnterpriseMonthlyLimitSer.SetData(model);
            return await Task.Run(()=>EnterpriseMonthlyLimitSer.Execute());
        }
        /// <summary>
        /// 编辑企业月限额状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("EditEnterpriseMonthStatus"), HttpPost]
        public async Task<ResponseMessageModel> EditEnterpriseMonthStatus([FromBody]RequestModel model)
        {
            EditEnterpriseMonthStatusSer.SetData(model);
            return await Task.Run(()=>EditEnterpriseMonthStatusSer.Execute());
        }

        /// <summary>
        /// 获取总授信额度
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("OverView"), HttpPost]
        public async Task<ResponseMessageModel> GetEnterpriseOverView([FromBody]RequestModel model)
        {
            GetEnterpriseOverViewSer.SetData(model);
            return await Task.Run(()=>GetEnterpriseOverViewSer.Execute());
        }

        /// <summary>
        /// 修改企业总授信额度和剩余额度
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("UpdateEnterpriseCreditAmount"), HttpPost]
        public async Task<ResponseMessageModel> UpdateCreditAmount([FromBody]RequestModel model)
        {
            UpdateEnterpriseCreditAmountSer.SetData(model);
            return await Task.Run(() => UpdateEnterpriseCreditAmountSer.Execute());
        }
    }
}
