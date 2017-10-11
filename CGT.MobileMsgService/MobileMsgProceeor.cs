using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CGT.MobileMsgService
{
    public class MobileMsgProceeor : ProcessorBase<View>
    {
        public override string RequestAddress
        {
            get { return "sendsms"; }
        }

        protected override string ServiceAddress
        {
            get { return "https://sms.reapal.com/sms/";}
        }

        private readonly string _content;
        private readonly string _phone;
        public MobileMsgProceeor(string phone,string content)
        {
            _content = content;
            _phone = phone;
        }
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", 0);
            result.Add("merchant_id", "000000000000000");
            result.Add("phone", _phone);
          //  result.Add("business_type", OperID);
           // result.Add("serialnumber", OperID);
           // result.Add("template_code", OperID);
            result.Add("sign_code", "P00061");
            result.Add("template_param", JsonConvert.SerializeObject(new {content=_content}));
            result.Add("sign_type", "md5");
            return result;
        }
    }
}
