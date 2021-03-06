﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 基类
    /// </summary>
    public class InsuranceViewBase
    {
        /// <summary>
        /// 返回码
        /// </summary> 
        public string result_code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary> 
        public string result_msg { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string data { get; set; }
    }

   
}
