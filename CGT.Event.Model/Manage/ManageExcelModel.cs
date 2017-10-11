using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Event.Model.Manage
{
    /// <summary>
    /// 差旅导入Excel 信息实体
    /// </summary>
    public class ManageExcelModel
    {
        /// <summary>
        /// 票面价
        /// </summary>
        public decimal TicketPrice { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 起飞时间
        /// </summary>
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// 出发机场三字码
        /// </summary>
        public string DepartCode { get; set; }
        /// <summary>
        /// 到达机场三字码
        /// </summary>
        public string ArriveCode { get; set; }
        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo { get; set; }
        /// <summary>
        /// 舱位
        /// </summary>
        public string Cabin { get; set; }
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string TicketNum { get; set; }
        /// <summary>
        /// 出票日期
        /// </summary>
        public DateTime TicketTime { get; set; }
        /// <summary>
        /// PNR
        /// </summary>
        public string PNR { get; set; }
        /// <summary>
        /// 企业信息
        /// </summary>
        public string Enterprise { get; set; }

    }
}
