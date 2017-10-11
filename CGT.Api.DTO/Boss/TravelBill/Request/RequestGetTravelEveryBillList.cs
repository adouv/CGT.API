using System;
using System.ComponentModel.DataAnnotations;
namespace CGT.Api.DTO.Boss.TravelBill
{
    /// <summary>
    /// 日账单实体
    /// </summary>
    public class RequestGetTravelEveryBillList : RequestBaseModel
    {
        /// <summary>
        /// 分销商名称
        /// </summary>
        public string PayCenterName { get; set; }
        /// <summary>
        /// 子商户Code
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int? EnterpriseId { get; set; }
        /// <summary>
        /// 账单状态 -1全部 0 未还款  1 还款
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 账期
        /// </summary>
        public string BillDate { get; set; }
        /// <summary>
        /// 开始账期
        /// </summary>
        public DateTime? StartBillDate { get; set; }

        /// <summary>
        /// 结束账期
        /// </summary>
        public DateTime? EndBillDate { get; set; }
        /// <summary>
        /// 生成账单开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 生成账单结束日期
        /// </summary>
        public string EndDate { get; set; }
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
    }
}
