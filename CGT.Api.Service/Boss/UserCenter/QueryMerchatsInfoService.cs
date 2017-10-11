using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.UserCenter.Service;
using Org.BouncyCastle.Utilities.IO;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 商户查询
    /// </summary>
    public class QueryMerchatsInfoService : ApiBase<RequsetMerchatsInfo>
    {
        #region 注入服务

        public MerchantsQueryProcessor merchantsQueryProcessor { get; set; }

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
            merchantsQueryProcessor.InitData(this.Parameter.CompanyName, this.Parameter.Status, this.Parameter.PageIndex,this.Parameter.PageSize, this.Parameter.CreateBeginTime, this.Parameter.CreateEndTime);
            var data = merchantsQueryProcessor.Execute();
            if (data.Success)
            {
                this.Result.Data = data.Result;
            }
            else
            {
                this.Result.IsSuccess = false;
                this.Result.Message = data.Message;
                this.Result.ErrorCode =data.MsgCode;
            }        
        }
    }
}
