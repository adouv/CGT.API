using PetaPoco.NetCore;
using System;


namespace CGT.Entity.CgtTravelModel
{
    /// <summary>
    /// 账单扩展
    /// </summary>
    public partial class Bill
    {
        /// <summary>
        /// 账单下载人类型()
        /// </summary>
        [ResultColumn]
        public int MerchantType { get; set; }
        /// <summary>
        ///  分销商利息
        /// </summary>
        [ResultColumn]
        public decimal DistributorInterest { get; set; }
        /// <summary>
        /// 还款状态
        /// </summary>
        [ResultColumn]
        public string StatusStr { get; set; }
        /// <summary>
        /// 账单类型
        /// </summary>
        [ResultColumn]
        public string BillTypeStr { get; set; }
        /// <summary>
        /// 宽限期开始日期
        /// </summary>
        [ResultColumn]
        public string GraceStartDate { get; set; }
        /// <summary>
        /// 宽限期结束日期
        /// </summary>
        [ResultColumn]
        public string GraceEndDate { get; set; }
        /// <summary>
        /// 宽限期天数
        /// </summary>
        [ResultColumn]
        public int? GraceDay { get; set; }
        /// <summary>
        /// 逾期开始日期
        /// </summary>
        [ResultColumn]
        public string OverdueStartDate { get; set; }
        /// <summary>
        /// 逾期结束日期
        /// </summary>
        [ResultColumn]
        public string OverdueEndDate { get; set; }
        /// <summary>
        /// 逾期天数
        /// </summary>
        [ResultColumn]
        public int? OverdueDay { get; set; }
        /// <summary>
        /// 账期
        /// </summary>
        [ResultColumn]
        public string BillDateStr { get; set; }

        /// <summary>
        /// 宽限期利率
        /// </summary>
        [ResultColumn]
        public decimal GraceBate { get; set; }

        /// <summary>
        /// 逾期利率
        /// </summary>
        [ResultColumn]
        public decimal OverdueBate { get; set; }

        /// <summary>
        /// 宽限期天数 
        /// </summary>
        [ResultColumn]
        public int? GraceCount { get; set; }

        [Ignore]
        public DateTime? GraceDate
        {
            get
            {
                if (GraceDay != null)
                {
                    return BillDate.AddDays((int)GraceDay);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
