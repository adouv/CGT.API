using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CGT.Api.DTO.Boss.TravelRisk
{
    /// <summary>
    /// 获取小何接口日志列表请求类
    /// </summary>
    public class RequestGetXHInterFaceCheckTicketResultLogList : RequestBaseModel
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [Required(ErrorMessage = "当前页码Pageindex必填")]
        public int Pageindex { get; set; }

        /// <summary>
        /// 页面尺寸
        /// </summary>
        [Required(ErrorMessage = "页面尺寸Pagesize必填")]
        public int Pagesize { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string StratDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate { get; set; }
    }
}
