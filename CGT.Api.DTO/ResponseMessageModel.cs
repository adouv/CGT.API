namespace CGT.Api.DTO
{
    /// <summary>
    /// api统一返回类
    /// </summary>
    public class ResponseMessageModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ResponseMessageModel()
        {
            ErrorCode = "0000";
            IsSuccess = true;
            Message = "成功";
        }


        /// <summary>
        /// 商户id
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 状态码
        /// 100成功，200失败
        /// </summary>
        public int Status
        {
            get
            {
                return IsSuccess ? 100 : 200;
            }
        }
    }
}
