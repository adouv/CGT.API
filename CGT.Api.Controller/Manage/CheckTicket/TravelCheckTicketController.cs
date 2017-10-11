using CGT.Api.DTO;
using CGT.Event.Model.Manage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CGT.Api.Service.Manage.CheckTicket;
using System.IO;
using Microsoft.AspNetCore.Http;
using CGT.DDD.MQ;
using Newtonsoft.Json;
using CGT.Api.DTO.Boss.TravelOrder.Request;
using System.Web;
using Microsoft.Extensions.Caching.Memory;
using CGT.DDD.Logger;

namespace CGT.Api.Controllers.Manage.CheckTicket
{
    /// <summary>
    /// boss差旅订单api
    /// </summary>
    [Produces("application/json")]
    [Route("api/manage/travelcheckticket")]
    [EnableCors("AllowSameDomain")]
    public class TravelCheckTicketController : BaseController
    {
        #region
        public TravelOrderImportService travelOrderImportService { get; set; }

        #endregion
        /// <summary>
        /// 验证票号
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("list"), HttpPost]
        public async Task<bool> GetTravelOrder()
        {
            return true;
        }

        [HttpPost]
        [Route("TravelOrderUpload")]
      
        public async Task<ResponseMessageModel> TravelOrderTemplateUpload([FromQuery] RequestTravelOrderUpload order)
        {


            if (23 == DateTime.Now.Hour)
            {
                throw new Exception("23:00-24:00为数据处理时间阶段，无法导单，敬请谅解！");
            }

            var requestBaseModel = new RequestModel
            {
                MerchantId = HttpContext.Request.Query["formData[MerchantId]"],
                EncryptKey = HttpContext.Request.Query["formData[EncryptKey]"],
                Data = HttpContext.Request.Query["formData[Data]"],

            };

            travelOrderImportService.SetData(requestBaseModel);
           
            travelOrderImportService.ExcelStream = HttpContext.Request.Body;

            return travelOrderImportService.Execute();

        }

        [HttpPost]
        [Route("TravelOrderUploadConfirm")]
        public async Task<ResponseMessageModel> ConfrimTravelOrderImport()
        {

            return new ResponseMessageModel();
        }
        /// <summary>
        /// 导入测试使用
        /// </summary>
        /// 
        /// <param name="model"></param>
        [HttpPost]
        [Route("TravelOrderImport")]
        public async Task<ResponseMessageModel> TravelOrderImport([FromBody]ManageTestModel model)
        {
            LoggerFactory.Instance.Logger_Info("压测获得参数：" + JsonConvert.SerializeObject(model), "TravelCheckTicket");

            RequestTravelOrderImportModel request = new RequestTravelOrderImportModel() { PayCenterCode = model.PayCenterCode, PayCenterName = model.PayCenterName, MerchantCode = model.MerchantCode };
            try
            {
                travelOrderImportService.SetData(null);
                travelOrderImportService.Parameter = request;
                travelOrderImportService.ExcelStream = new MemoryStream(model.Excel);
                return travelOrderImportService.Execute();
            }
            catch (Exception ex)
            {

                LoggerFactory.Instance.Logger_Error(ex, "TravelCheckTicket");
                return null;
            }                         
            
        }
    }


}
