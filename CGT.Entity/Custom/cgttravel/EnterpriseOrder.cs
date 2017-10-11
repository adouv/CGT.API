using PetaPoco.NetCore;
using System;

namespace CGT.Entity.CgtTravelModel {
    /// <summary>
    /// 差旅订单实体扩展类
    /// </summary>
    public partial class EnterpriseOrder {
        [ResultColumn] public long EnterpriseOrderRiskId { get; set; }

        [ResultColumn] public string TravelBatchId { get; set; }

        [ResultColumn] public int? TravelRiskType { get; set; }

        [ResultColumn] public int? TravelRiskState { get; set; }

        [ResultColumn] public DateTime? RiskCreateTime { get; set; }

        [ResultColumn] public int? ReviewState { get; set; }

        [ResultColumn] public long? ReviewUserId { get; set; }

        [ResultColumn] public string FailReason { get; set; }

        [ResultColumn] public string RefuseReason { get; set; }
        [ResultColumn] public DateTime ReviewTime { get; set; }
        [ResultColumn] public string EnterpriseName { get; set; }
        [ResultColumn] public string ReviewName { get; set; }
        [ResultColumn] public string BackStatusStr{ get; set; }
        [ResultColumn] public DateTime? RealpayDateTime { get; set; }

    }
}
