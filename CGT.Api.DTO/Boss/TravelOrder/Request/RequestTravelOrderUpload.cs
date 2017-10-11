using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CGT.Api.DTO.Boss.TravelOrder.Request
{
  public  class RequestTravelOrderUpload
    {
        public string id { get; set; }
        public string name { get; set; }

        public int size { get; set; }

        public Stream inputstream { get; set; }

    }
}
