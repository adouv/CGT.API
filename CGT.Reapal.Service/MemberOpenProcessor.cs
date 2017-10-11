using CGT.DDD.Config;
using System.Collections.Specialized;
namespace CGT.Reapal.Service
{
    /// <summary>
    /// 融宝开户
    /// </summary>
    public class MemberOpenProcessor : ProcessorBase<MemberOpenView>
    {
        protected override string RequestAddress
        {
            get { return "member/open"; }
        }

        protected override string ServiceAddress
        {
            get { return JsonConfig.JsonRead("ReapalMemberApiUrl", "Reapal"); }
        }

        private readonly string _member_id;
        private readonly string _member_ip;
        private readonly string _email;
        private readonly string _mobile;
        private readonly string _real_name;
        private readonly string _cert_no;
        private readonly string _login_pwd;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_id">用户在采购通的id</param>
        /// <param name="member_ip">ip</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">电话</param>
        /// <param name="real_name">真实姓名</param>
        /// <param name="cert_no">银行卡号</param>
        /// <param name="login_pwd">登录密码（和采购通一样）</param>
        public MemberOpenProcessor(string member_id, string member_ip, string email, string real_name, string cert_no, string login_pwd)
        {
            _member_id = member_id;
            _member_ip = member_ip;
            _email = email;
            //_mobile = mobile;
            _real_name = real_name;
            _cert_no = cert_no;
            _login_pwd = login_pwd;
        }

        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("member_id", _member_id);
            result.Add("member_ip", _member_ip);
            result.Add("email", _email);
            //result.Add("mobile", _mobile);
            result.Add("real_name", _real_name);
            result.Add("cert_no", _cert_no);
            result.Add("login_pwd", _login_pwd);
            return result;
        }
    }
}
