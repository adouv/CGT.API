using PetaPoco.NetCore;
using System;

namespace CGT.Entity.CgtModel {
    public partial class UserAccount {
        [ResultColumn]
        public string TicketAmount { get; set; }

        [ResultColumn]
        public int? BillDateTime { get; set; }
    }
}
