using CGT.Api.DTO;
using CGT.Api.Service.Boss.Staff;
using CGT.DDD.Config;
using CGT.DDD.Utils;
using CGT.DDD.Utils.File;
using CGT.DDD.Utils.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace CGT.Api.Controllers.Boss.Travel
{
    /// <summary>
    /// BOSS差旅员工白名单API
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/TravelStaff")]
    [EnableCors("AllowSameDomain")]
    public class StaffController : BaseController
    {
        #region 注入服务

        public ImportStaffService ImportStaffSer { get; set; }

        public QueryStaffService QueryStaffSer { get; set; }

        public SaveStaffService SaveStaffSer { get; set; }

        #endregion

        /// <summary>
        /// 企业员工模板下载页面
        /// </summary>
        /// <returns></returns>
        [Route("DownloadStaffExcel"), HttpPost]
        public ResponseMessageModel DownloadStaffExcel()
        {
            try
            {
                string fileconfig = JsonConfig.JsonRead("EnterpriseStaffTemplate","CGTExcel");
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
        /// 上传企业员工白名单
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        [Route("ImportStaffList"), HttpPost]
        public ResponseMessageModel ImportStaffList(IFormFile File)
        {
            RequestModel model = JsonConvert.DeserializeObject<RequestModel>(MyHttpContext.Current.Request.Form["formData"]);
            ImportStaffSer.SetData(model);
            ImportStaffSer.SetFileData(File);
            return ImportStaffSer.Execute();
        }

        /// <summary>
        /// 查询企业员工列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("QueryStaffList"), HttpPost]
        public ResponseMessageModel QueryStaffList([FromBody]RequestModel model)
        {
            QueryStaffSer.SetData(model);
            return QueryStaffSer.Execute();
        }

        /// <summary>
        /// 修改企业员工信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("SaveStaff"), HttpPost]
        public ResponseMessageModel SaveStaff([FromBody]RequestModel model)
        {
            SaveStaffSer.SetData(model);
            return SaveStaffSer.Execute();
        }
    }
    
}
