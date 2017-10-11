using PetaPoco.NetCore;
using System;

namespace CGT.Entity.CgtTravelModel {
    /// <summary>
    /// 企业白名单
    /// </summary>
    public partial class EnterpriseWhiteList {
        [ResultColumn] public long UserId { get; set; }

        [ResultColumn] public string UserName { get; set; }

        [ResultColumn] public DateTime? BeginDate { get; set; }
        [ResultColumn] public DateTime? EndDate { get; set; }

        [ResultColumn] public decimal TicketAmount { get; set; }

        [ResultColumn]
        public string ModifyTimes {
            get => this.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
