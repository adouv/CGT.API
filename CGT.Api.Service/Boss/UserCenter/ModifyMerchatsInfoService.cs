﻿using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.UserCenter.Service;
using Org.BouncyCastle.Utilities.IO;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 商户修改
    /// </summary>
    /// 
    public class ModifyMerchatsInfoService : ApiBase<RequsetMerchatsModifyInfo>
    {
        #region 注入服务
        public MerchantsInfoModifyProcessor merchantsModifyProcessor { get; set; }

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
            merchantsModifyProcessor.InitData(
                this.Parameter.loginName, this.Parameter.password,
                this.Parameter.companyCode, this.Parameter.companyName,
                this.Parameter.receivablesAccount, this.Parameter.merchantNo,
                this.Parameter.telphone, this.Parameter.address,
                this.Parameter.email, this.Parameter.reapalPassword,
                this.Parameter.contactPerson);
            var data = merchantsModifyProcessor.Execute();
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
