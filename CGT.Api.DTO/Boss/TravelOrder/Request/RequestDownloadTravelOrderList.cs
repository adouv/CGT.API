using System;

namespace CGT.Api.DTO.Boss.TravelOrder {
    /// <summary>
    /// 差旅订单下载请求实体
    /// </summary>
    public class RequestDownloadTravelOrderList : RequestBaseModel {

        public int pageindex { get; set; }
        public int pagesize { get; set; }
        /// <summary>
        /// 商户code 
        /// </summary>
        public string MerchantCode { get; set; }
        /// <summary>
        /// 订单状态（-1全部，0未还，1账单还款，2提前还款，3逾期还款）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public int? EnterpriseId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 账单编号
        /// </summary>
        public int? BillId { get; set; }

        /// <summary>
        /// 开始返现时间
        /// </summary>
        public DateTime? StartBillDate { get; set; }

        /// <summary>
        /// 结束返现时间
        /// </summary>
        public DateTime? EndBillDate { get; set; }


        public string OrderTravelBatchId { get; set; }

        public  string DataSouce { get; set; }

        public  string PayCenterCode { get; set; }

        public  int? BackStatus { get; set; }

    }
}
