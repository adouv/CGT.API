using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Entity.CgtTicketModel
{
    public partial class InterRefund
    {
        /// <summary>
        /// order表中orderId
        /// </summary>
        [ResultColumn]
       public string OrderOrderId { get; set; }
    }
}
