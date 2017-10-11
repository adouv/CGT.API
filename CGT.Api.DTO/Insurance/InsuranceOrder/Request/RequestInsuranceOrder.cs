using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CGT.Api.DTO.Insurance.InsuranceOrder.Request
{
    public class RequestInsuranceOrder : RequestBaseModel
    {
        public string UserId { get; set; }
        public string OthOrderCode { get; set; }
        public string TotalAmount { get; set; }
        public string TotalPremium { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AppliName { get; set; }
        public string IdentifyType { get; set; }
        public string IdentifyNumber { get; set; }
        public string flightNo { get; set; }
        public string flightDate { get; set; }
        public string Mobile { get; set; }
        public List<InsurancedPassenger> InsuredPersonList { get; set; }
    }
    public class InsurancedPassenger
    {
        public int applyNum { get; set; }
        public string InsuredName { get; set; }
        public string IdentifyType { get; set; }
        public string IdentifyNumber { get; set; }
        public string Relation { get; set; }
        public string Mobile { get; set; }
    }
}
