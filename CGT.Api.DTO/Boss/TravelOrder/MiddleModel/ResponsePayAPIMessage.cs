using System;

namespace CGT.Api.DTO.Boss.TravelOrder.MiddleModel {
    public class ResponsePayAPIMessage
    {
        string _serializableFields;
        /// <summary>
        /// 初始化ResponseMessage
        /// </summary>
        public ResponsePayAPIMessage()
            : this(null)
        { }
        /// <summary>
        /// 初始化ResponseMessage
        /// </summary>
        /// <param name="serializableFields">希望返回的字段</param>
        public ResponsePayAPIMessage(string serializableFields)
        {
            this._serializableFields = serializableFields;
            this.Status = 100;
            this.GuidKey = Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// 标示码，与RequestBase里的GuidKey对应
        /// </summary>
        public string GuidKey { get; set; }
        /// <summary>
        /// 状态码
        /// 100成功，200失败
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 业务错误代码
        /// </summary>
        public string MessageCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 结果集json字符串
        /// </summary>
        public string Result { get; set; }

    }
}
