using CGT.DDD.Config;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 登录
    /// </summary>
    public class MemberLoginProcessor : ProcessorBase<MemberLoginView>
    {
        protected override string RequestAddress
        {
            get { return "member/login"; }
        }

        protected override string ServiceAddress
        {
            get { return JsonConfig.JsonRead("ReapalMemberApiUrl","Reapal"); }
        }

        private readonly string _member_ip;
        private readonly string _email;
        private readonly string _login_pwd;
        
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="email"></param>
        /// <param name="login_pwd"></param>
        public MemberLoginProcessor(string member_ip, string email, string login_pwd)
        {
            _member_ip = member_ip;
            _email = email;
            _login_pwd = login_pwd;
        }


        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("member_ip", _member_ip);
            result.Add("email", _email);
            result.Add("login_pwd", _login_pwd);
            return result;
        }
    }
}
