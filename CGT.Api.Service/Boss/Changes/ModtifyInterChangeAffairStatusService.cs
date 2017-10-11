using CGT.Api.DTO;
using CGT.Api.DTO.Boss.InterRefund;
using CGT.Entity.CgtTicketModel;
using CGT.PetaPoco.Repositories.CgtTicket;
namespace CGT.Api.Service.Boss.Changes
{
    /// <summary>
    ///  国际票改期修改锁单状态
    /// </summary>
    public class ModtifyInterChangeAffairStatusService : ApiBase<RequestInterRefundAffairStatus>
    {
        #region 注入仓储
        public InterChangeRep interChangeRep { get; set; }
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
            var result = interChangeRep.ModtifyInterChangeAffairStatus(new InterChange { OrderID= this.Parameter.LocalId,AffairStatus=this.Parameter.AffairStatus });
            if (result>0)
            {
                this.Result.IsSuccess = true;
            }
            else
            {
                throw new System.Exception("插入数据库失败");
            }
        }
    }
}
