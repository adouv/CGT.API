using PetaPoco.NetCore;

namespace CGT.Entity.CgtTravelModel
{
    /// <summary>
    /// 日账单
    /// </summary>
    public partial class BillEveryDay
    {
        /// <summary>
        /// 状态描述
        /// </summary>
        [ResultColumn]
        public string StatusStr { get; set; }
        /// <summary>
        /// 账期
        /// </summary>
        [ResultColumn]
        public string BillDateStr { get; set; }
        /// <summary>
        /// 分销商利息
        /// </summary>
        [ResultColumn]
        public decimal DistributorInterest { get; set; }
    }
}
