
using CGT.Api.DTO;
using CGT.PetaPoco.Repositories.Insurance;
using CGT.Api.DTO.Insurance.InsuranceOrder.Request;

namespace CGT.Api.Service.Insurance
{
    public class PageInsuranceService : ApiBase<RequestQueryInsuranceOrder>
    {
        #region 注入服务
        public InsuranceOrderRep insuranceOrderRep { get; set; }
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
        protected override void ExecuteMethod()
        {
          var result=  insuranceOrderRep.PageInsuranceOrder(this.Parameter.pageindex, this.Parameter.pagesize, new Entity.CgtInsuranceModel.InsuranceOrder {
                   UserId = this.Parameter.UserId,
                   OthOrderCode=this.Parameter.OthOrderCode,
            },this.Parameter.StartDate,this.Parameter.EndDate);

            this.Result.Data = result;
        }
    }
}
