using CGT.DDD.Encrpty;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.CheckTicket.Service
{
    /// <summary>
    /// 贷前注册接口
    /// </summary>
    public class PreRegistrationProcessor : ProcessorBase<PreRegistrationRequestView>
    {
        protected override string RequestAddress
        {
            get { return "webapi/TicketVerify/Register"; }
        }

        protected override string ServiceAddress
        {
            get { return CheckTicketApiUrl; }
        }
        private readonly string _Hashcode;
        private readonly string _NotifyURL;
        private readonly string _uuid;
        private readonly List<PreRegistrationRequestView> _lstticket;
        private readonly string _Key;
        private readonly string _sign;

        public PreRegistrationProcessor(List<PreRegistrationRequestView> lstticket)
        {
            _Hashcode = CheckTicketHashcode;
            _NotifyURL = CheckTicketNotifyURL;
            _uuid = "";
            _lstticket = lstticket;
            _Key = CheckTicketKey;
            string str = _Hashcode + _NotifyURL + _uuid + _Key + DateTime.Now.ToString("yyyyMMdd");
            _sign = Encrpty.MD5Encrypt(str).ToLower();
            
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("lstticket", _lstticket);
            result.Add("Hashcode", _Hashcode);
            result.Add("sign", _sign);
            result.Add("uuid", _uuid);
            result.Add("NotifyURL", _NotifyURL);
            return result;
        }

    }
}
