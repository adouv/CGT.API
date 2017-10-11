using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.UserCenter.Service;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 修改商户状态
    /// </summary>
    public class ModifyMerchatsStatusService : ApiBase<RequsetMerchatsModifyStatus>
    {
        #region 注入服务

        public MerchantsStatusModifyProcessor merchantsStatusModifyProcessor { get; set; }

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
            merchantsStatusModifyProcessor.InitData(this.Parameter.status, this.Parameter.companyCode);
            var data = merchantsStatusModifyProcessor.Execute();
          
            if (data.Success)
            {
                this.Result.IsSuccess = true;
            }
            else
            {
                this.Result.IsSuccess = false;
                this.Result.Message = data.Message;
                this.Result.ErrorCode = data.MsgCode;
            }
        }
    }
}
