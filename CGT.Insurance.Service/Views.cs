using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Insurance.Service
{
    /// <summary>
    /// 基类
    /// </summary>
    public class ViewBase
    {
        /// <summary>
        /// 返回码
        /// </summary> 
        public string MessageCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary> 
        public string Message { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Result { get; set; }
    }
}
