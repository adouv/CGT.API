using CGT.DDD.Config;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 融宝绑卡
    /// </summary>
    public class MemberBindCardProcessor: ProcessorBase<MemberBindCardView>
    {
        protected override string RequestAddress
        {
            get { return "member/bindcard"; }
        }

        protected override string ServiceAddress
        {
            get { return JsonConfig.JsonRead("ReapalMemberApiUrl", "Reapal"); }
        }

        private readonly string _member_no;
        private readonly string _member_ip;
        private readonly string _card_no;
        private readonly string _phone;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_no"></param>
        /// <param name="member_ip"></param>
        /// <param name="card_no"></param>
        /// <param name="phone"></param>
        public MemberBindCardProcessor(string member_no, string member_ip, string card_no, string phone)
        {
            _member_no = member_no;
            _member_ip = member_ip;
            _card_no = card_no;
            _phone = phone;
        }


        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("member_no", _member_no);
            result.Add("member_ip", _member_ip);
            result.Add("card_no", _card_no);
            result.Add("phone", _phone);
            return result;
        }
    }
}
