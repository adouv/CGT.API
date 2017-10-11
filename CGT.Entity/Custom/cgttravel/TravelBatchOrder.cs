using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Entity.CgtTravelModel
{
  public partial  class TravelBatchOrder
    {
        [ResultColumn]
        public string EnterpriseName { get; set; }
    }
}
