using CGT.Api.DTO;
using CGT.Api.DTO.Boss.InterRefund;
using CGT.Entity.CgtTicketModel;
using CGT.PetaPoco.Repositories.CgtTicket;
using CGT.PetaPoco.Repositories.CgtUser;
using System;
using static CGT.DDD.Enums.EnumHelper;

namespace CGT.Api.Service.Boss.Changes
{
    /// <summary>
    ///  国际票提交改期
    /// </summary>
    public class SubmitChangeService : ApiBase<RequestSubmitInterRefund>
    {
        #region 注入仓储
        public InterChangeRep interChangeRep { get; set; }
        public UserRep userRep { get; set; }
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
            var user = userRep.GetUser(new Entity.CgtUserModel.User { UserId = this.Parameter.UserId });
            var result = interChangeRep.UpdateInterChangeAndInterChangeRemark(new InterChange
            {
                ModifyUserId = this.Parameter.UserId,
                ModifyUserName = user.UserName,
                ModifyTime = DateTime.Now,
                OrderID = this.Parameter.LocalId,
                Status = AltTicketStatus.AltTicketing.GetHashCode()
            }, new InterChangeRemark
            {
                CreateUserId = this.Parameter.UserId,
                CreateUserName = user.UserName,
                CreateTime = DateTime.Now,
                OrderId = this.Parameter.LocalId,
                Remark = this.Parameter.Remark

            });
        }
    }
}
