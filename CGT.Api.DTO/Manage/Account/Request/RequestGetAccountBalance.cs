using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CGT.Api.DTO.Manage.Account
{
    /// <summary>
    /// 获取账户余额请求实体
    /// </summary>
    public class RequestGetAccountBalance: RequestBaseModel
    {
        /// <summary>
        ///融宝商户编号
        /// </summary>
        [Required(ErrorMessage = "融宝商户编号必填")]
        public string ReapalMerchantId { get; set; }
    }
}
