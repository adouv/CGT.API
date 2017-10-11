using CGT.Api.DTO;
using CGT.Api.Service.Insurance;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CGT.Api.Controllers.Insurance
{
    /// <summary>
    /// 保险用户
    /// </summary>
    [Produces("application/json")]
    [Route("api/Insurance")]
    [EnableCors("AllowSameDomain")]
    public class InsuranceUserController : BaseController
    {
        public InsuranceUserService insuranceUserService { get; set; }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("Login"), HttpPost]
        public async Task<ResponseMessageModel> Login([FromBody]RequestModel model)
        {
            insuranceUserService.SetData(model);
            return await Task.Run(() => insuranceUserService.Execute());
        }

    }
}
