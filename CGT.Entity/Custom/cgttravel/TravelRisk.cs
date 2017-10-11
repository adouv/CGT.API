using PetaPoco.NetCore;
using System;

namespace CGT.Entity.CgtTravelModel{
    /// <summary>
    /// 风控规则类
    /// </summary>
    public partial class TravelRisk {
        /// <summary>
        /// 企业名称
        /// </summary>
        [ResultColumn]
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        [ResultColumn]
        public string ModifyName { get; set; }

    }
}
