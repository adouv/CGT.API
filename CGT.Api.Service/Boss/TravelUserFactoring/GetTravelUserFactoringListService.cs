using System;
using System.Collections.Generic;
using System.Text;
using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelUserFactoring;
using CGT.PetaPoco.Repositories.CgtTravel;

namespace CGT.Api.Service.Boss.TravelUserFactoring
{
    /// <summary>
    /// 金主列表
    /// </summary>
    public class GetTravelUserFactoringListService : ApiBase<RequestTravelUserFactoringListModel>
    {
        public TravelUserFactoringRep userFactoringRep { get; set; }

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
            var result = userFactoringRep.GetUserFactoringList(this.Parameter.FactoringName, this.Parameter.FactoringCode);
            this.Result.Data = result;
        }
    }
}
