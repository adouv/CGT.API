using CGT.DDD.Config;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 融宝账户修改
    /// </summary>
    public class ModifyContractInfoProcessor : ProcessorBase<ModifyContractInfoView>
    {
        protected override string RequestAddress
        {
            get { return "member/modifycontractinfo"; }
        }

        protected override string ServiceAddress
        {
            get { return JsonConfig.JsonRead("ReapalMemberApiUrl","Reapal"); }
        }

        private readonly string _member_ip;
        private readonly string _member_no;
        private readonly string _mobile;
        private readonly string _email;
        private readonly string _login_pwd;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="member_no"></param>
        public ModifyContractInfoProcessor(string member_ip, string member_no, string mobile, string email, string login_pwd)
        {
            _member_ip = member_ip;
            _member_no = member_no;
            _mobile = mobile;
            _email = email;
            _login_pwd = login_pwd;
        }

        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("member_ip", _member_ip);
            result.Add("member_no", _member_no);
            result.Add("mobile", _mobile);
            result.Add("email", _email);
            result.Add("login_pwd", _login_pwd);
            return result;
        }
    }
}
