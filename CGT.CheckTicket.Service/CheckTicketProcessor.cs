using CGT.DDD.Encrpty;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.CheckTicket.Service
{
    /// <summary>
    /// 查询机票验证结果
    /// </summary>
    public class CheckTicketProcessor : ProcessorBase<PreRegistrationRequestView>
    {
        protected override string RequestAddress
        {
            get { return "webapi/TicketVerify/query" + "?hashcode=" + _Hashcode + "&uuid=" + _uuid + "&sign=" + _sign + "&outstyle=" + _outstyle; }
        }

        protected override string ServiceAddress
        {
            get { return CheckTicketApiUrl; }
        }
        private readonly string _Hashcode;
        private readonly string _uuid;
        private readonly string _outstyle;
        private readonly string _Key;
        private readonly string _sign;

        public CheckTicketProcessor(string uuid)
        {
            _Hashcode = CheckTicketHashcode;
            _uuid = uuid;
            _outstyle = "4";
            _Key = CheckTicketKey;
            _sign = Encrpty.MD5Encrypt(_Hashcode + _outstyle + _uuid + _Key + DateTime.Now.ToString("yyyyMMdd")).ToLower();
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("Hashcode", _Hashcode);
            result.Add("uuid", _uuid);
            result.Add("sign", _sign);
            result.Add("outstyle", _outstyle);
            return result;
        }

    }
}
