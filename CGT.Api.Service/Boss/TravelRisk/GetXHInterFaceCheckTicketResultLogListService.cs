using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelRisk;
using CGT.PetaPoco.Repositories.CgtLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.TravelRisk
{
    public class GetXHInterFaceCheckTicketResultLogListService : ApiBase<RequestGetXHInterFaceCheckTicketResultLogList>
    {
        #region 注入服务
        public XHInterFaceCheckTicketResultLogRep xhInterFaceCheckTicketResultLogRep { get; set; }
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
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod()
        {
            string EndDate = "";
            if (!string.IsNullOrWhiteSpace(Parameter.EndDate))
            {
                EndDate = Convert.ToDateTime(Parameter.EndDate).AddDays(1).ToString("yyyy-MM-dd");
            }
            var data = xhInterFaceCheckTicketResultLogRep.GetXHInterFaceCheckTicketResultLogList(Parameter.StratDate, EndDate, Parameter.Pageindex, Parameter.Pagesize);
            this.Result.Data = data;
        }
    }
}
