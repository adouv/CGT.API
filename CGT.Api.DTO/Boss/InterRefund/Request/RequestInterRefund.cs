using System;
using System.ComponentModel.DataAnnotations;
namespace CGT.Api.DTO.Boss.InterRefund
{
    /// <summary>
    /// 国际票退票提交请求实体
    /// </summary>
    public class RequestInterRefund : RequestBaseModel
    {

        public long UserId { get; set; }
       
        /// <summary>
        /// 当前页码
        /// </summary>
        [Required(ErrorMessage = "当前页码PageIndex必填")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面尺寸
        /// </summary>
        [Required(ErrorMessage = "页面尺寸PageSize必填")]
        public int PageSize { get; set; }


        /// <summary>
        /// 本地订单号
        /// </summary>
        public string LocalId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 所单状态
        /// </summary>
        public int AffairStatus { get; set; }
        /// <summary>
        /// 退票状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

    }
}
