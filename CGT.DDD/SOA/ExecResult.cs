namespace CGT.DDD.SOA
{
    /// <summary>
    /// 公共数据返回类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ExecResult<TResult>
    {
        /// <summary>
        /// 请求/执行是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 消息
        /// 暂时用于存放错误原因
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息代码
        /// </summary>
        public string MsgCode { get; set; }
        /// <summary>
        /// 结果信息
        /// </summary>
        public TResult Result { get; set; }
    }
}
