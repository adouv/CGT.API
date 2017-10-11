using CGT.DDD.Config;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 用户余额
    /// </summary>
    public class MemberAccountProcessor: ProcessorBase<MemberAccountView>
    {
        protected override string RequestAddress
        {
            get { return "member/account"; }
        }

        protected override string ServiceAddress
        {
            get { return JsonConfig.JsonRead("ReapalMemberApiUrl","Reapal"); }
        }


        private readonly string _member_ip;
        private readonly string _member_no;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="member_no"></param>
        public MemberAccountProcessor(string member_ip, string member_no)
        {
            _member_ip = member_ip;
            _member_no = member_no;
        }


        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("member_ip", _member_ip);
            result.Add("member_no", _member_no);
            return result;
        }
    }
}
