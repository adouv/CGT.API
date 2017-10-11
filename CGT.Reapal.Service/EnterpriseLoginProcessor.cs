using CGT.DDD.Config;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 商户登录校验
    /// </summary>
    public class EnterpriseLoginProcessor : ProcessorBase<MemberLoginView>
    {
        protected override string RequestAddress
        {
            get { return "merchant/checklogin"; }
        }

        protected override string ServiceAddress
        {
            get
            {
                return JsonConfig.JsonRead("ReapalEnterApiUrl","Reapal");
            }
        }

        private readonly string _member_ip;
        private readonly string _email;
        private readonly string _login_pwd;
        private readonly string _merchant_id;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="email"></param>
        /// <param name="login_pwd"></param>
        /// <param name="merchant_id"></param>
        public EnterpriseLoginProcessor(string Enter_ip, string email, string login_pwd, string merchant_id)
        {
            _member_ip = Enter_ip;
            _email = email;
            _login_pwd = login_pwd;
            _merchant_id = merchant_id;
        }


        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("login_merchantid", _merchant_id);
            result.Add("member_ip", _member_ip);
            result.Add("login_email", _email);
            result.Add("login_pwd", _login_pwd);
            return result;
        }
    }
}
