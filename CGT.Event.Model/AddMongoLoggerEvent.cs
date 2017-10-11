using CGT.DDD.Events;

namespace CGT.Event.Model
{
    /// <summary>
    /// 添加mango日志
    /// </summary>
    public class AddMongoLoggerEvent : EventBase
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        public string GuidKey { get; set; }
        /// <summary>
        /// 解密前请求数据
        /// </summary>
        public string RequestEnData { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequsetDeData { get; set; }
        /// <summary>
        /// 返回参数
        /// </summary>
        public object ResponseData { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Mesasge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ActionNmae { get; set; }
        /// <summary>
        /// 错误位置
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>

        public string CreateTime { get; set; }
    }
}
