using System.ComponentModel.DataAnnotations;
using System;

namespace CGT.Api.DTO {
    /// <summary>
    /// 统一提交
    /// </summary>
    public class RequestModel {
        private string guidkey;
        /// <summary>
        /// 标示码
        /// </summary>
        public string GuidKey {
            get => guidkey ?? Guid.NewGuid().ToString();
            set => guidkey = value;
        }
        /// <summary>
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
