using System;
using System.ComponentModel.DataAnnotations;
namespace CGT.Api.DTO.Boss.TravelBill
{
    public class RequestGetTravelBillList : RequestBaseModel
    {
        /// <summary>
        /// 分销商名称
        /// </summary>
        public string PayCenterName { get; set; }
        /// <summary>
        /// 子商户code 
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int? EnterpriseId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 0代理1分销2金主3财务
        /// </summary>
        public int MerchantType { get; set; }
        /// <summary>
        /// 开始账期
        /// </summary>
        public DateTime? StartBillDate { get; set; }
        /// <summary>
        /// 结束账期
        /// </summary>
        public DateTime? EndBillDate { get; set; }
        /// <summary>
        /// 还款状态（-1全部0未还1已换）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 账单类型  0正常  （1.宽限期  2逾期）
        /// </summary>
        public string BillType { get; set; }
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
