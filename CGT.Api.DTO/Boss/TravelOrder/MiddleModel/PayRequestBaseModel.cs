using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CGT.Api.DTO.Boss.TravelOrder.MiddleModel {
    /// <summary>
    /// 统一提交
    /// </summary>
    public class PayRequestBaseModel {
        // <summary>
        /// 商户code
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string MerchantId { get; set; }
        /// <summary>
        /// AES加密后的json数据
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string Data { get; set; }
        /// <summary>
        /// AESkey(证书加密后的)
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string EncryptKey { get; set; }
    }
}
