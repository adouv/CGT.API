using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Entity.CgtInsuranceModel {
    public partial class InsuranceOrder {
        [ResultColumn]
        public string InsuredName { get; set; }
        [ResultColumn]
        public string InsurdIdentifyNumber { get; set; }
        [ResultColumn]
        public string ApplyNum { get; set; }
        [ResultColumn]
        public string OrderOrderId { get; set; }
        [ResultColumn]
        public string IdentifyTypeName { get; set; }
        [ResultColumn]
        public string InsurdIdentifyTypeName { get; set; }
        [ResultColumn]
        public string RelationName { get; set; }
        /// <summary>
        /// 被保人手机号
        /// </summary>
        [ResultColumn]
        public string InsurdMobile { get; set; }

    }
}
