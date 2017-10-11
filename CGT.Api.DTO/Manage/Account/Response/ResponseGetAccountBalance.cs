using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Manage.Account.Response
{
    /// <summary>
    /// 获取商户余额信息返回实体
    /// </summary>
    public class ResponseGetAccountBalance
    {
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
    }
}
