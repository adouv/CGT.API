using CGT.Event.Model.Manage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Travel.Test.Service
{
    public class ImportProcessor : BaseProcessor<ManageRiskModel>
    {
        protected override string RequestAddress
        {
            get { return "api/manage/travelcheckticket/TravelOrderImport"; }
        }

        protected override string ServiceAddress
        {
            get { return "http://123.57.7.161:5002"; }
        }
        
        private readonly byte[] _Excel;
        private readonly string _PayCenterCode;
        private readonly string _PayCenterName;
        private readonly string _MerchantCode;

        public ImportProcessor(byte[] Excel, string PayCenterCode, string PayCenterName, string MerchantCode)
        {
            _Excel = Excel;
            _PayCenterCode = PayCenterCode;
            _PayCenterName = PayCenterName;
            _MerchantCode = MerchantCode;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("Excel", _Excel);
            result.Add("PayCenterCode", _PayCenterCode);
            result.Add("PayCenterName", _PayCenterName);
            result.Add("MerchantCode", _MerchantCode);
            return result;
        }
    }
}
