using CGT.Api.DTO;
using CGT.Api.Service.Boss.Changes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace CGT.Api.Controllers.Boss.Changes
{
    /// <summary>
    /// 采购通国际票退票
    /// </summary>
    [Produces("application/json")]
    [Route("api/Boss/Changes")]
    [EnableCors("AllowSameDomain")]
    public class InterChangeController : BaseController
    {
        public GetInterChangeService getInterChangeService { get; set; }

        public GetInterChangeDetailService getInterChangeDetailService { get; set; }

        public ModtifyInterChangeAffairStatusService modtifyInterChangeAffairStatusService { get; set; }

       public SubmitChangeService submitChangeService { get; set; }
        /// <summary>
        /// 国际票改期列表
        /// </summary>
        /// <param name="model">加密公共实体</param>
        /// <returns></returns>
        [Route("InterChangeList"), HttpPost]
        public ResponseMessageModel GetInterChangeList([FromBody]RequestModel model)
        {
            getInterChangeService.SetData(model);
            return getInterChangeService.Execute();
        }
        /// <summary>
        /// 改期详单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("InterChangeDetail"), HttpPost]
        public ResponseMessageModel GetInterChangeDetail([FromBody]RequestModel model)
        {
            getInterChangeDetailService.SetData(model);
            return getInterChangeDetailService.Execute();
        }
        /// <summary>
        /// 修改锁单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ModtifyInterChangeAffairStatus"), HttpPost]
        public ResponseMessageModel ModtifyInterChangeAffairStatus([FromBody]RequestModel model)
        {
            modtifyInterChangeAffairStatusService.SetData(model);
            return modtifyInterChangeAffairStatusService.Execute();
        }
        /// <summary>
        /// 提交改期
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("SubmitChange"), HttpPost]
        public ResponseMessageModel SubmitChange([FromBody]RequestModel model)
        {
            submitChangeService.SetData(model);
            return submitChangeService.Execute();
        }
    }
}
