using CGT.Api.DTO;
using CGT.Api.Service.Insurance;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CGT.Api.Controllers.Insurance
{
    /// <summary>
    /// 保险
    /// </summary>
    [Produces("application/json")]
    [Route("api/Insurance")]
    [EnableCors("AllowSameDomain")]
    public class InsuranceController : BaseController
    {
        public BuyInsureService buyInsureService { get; set; }

        public DownLoadInsuranceService downLoadInsuranceService { get; set; }


        public PageInsuranceService pageInsuranceService { get; set; }

        /// <summary>
        /// 买保险
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("BuyInsure"), HttpPost]
        public async Task<ResponseMessageModel> BuyInsure([FromBody]RequestModel model)
        {
            buyInsureService.SetData(model);
            return await Task.Run(() => buyInsureService.Execute());
        }
        /// <summary>
        /// 查询保险订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("PageInSureOrder"), HttpPost]
        public async Task<ResponseMessageModel> PageInSureOrder([FromBody]RequestModel model)
        {
            pageInsuranceService.SetData(model);
            return await Task.Run(() => pageInsuranceService.Execute());
        }
        /// <summary>
        /// 下载保险订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DownLoadInSureOrder"), HttpPost]
        public async Task<ResponseMessageModel> DownLoadInSureOrder([FromBody]RequestModel model)
        {
            downLoadInsuranceService.SetData(model);
            return await Task.Run(() => downLoadInsuranceService.Execute());
        }
    }
}
