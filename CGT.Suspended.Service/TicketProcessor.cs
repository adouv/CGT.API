
namespace CGT.SuspendedService {
    public class TicketProcessor : ProcessorBase<ViewBase> {
        private readonly CommandArgs _commandArgs;
        private readonly string _suspendedServiceUrl;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="email"></param>
        /// <param name="login_pwd"></param>
        /// <param name="merchant_id"></param>
        public TicketProcessor(CommandArgs commandArgs, string suspendedServiceUrl) {
            _commandArgs = commandArgs;
            _suspendedServiceUrl = suspendedServiceUrl;
        }

        protected override string ServiceAddress {
            get { return _suspendedServiceUrl; }
        }
        protected override string PrepareRequestCore() {
            return Newtonsoft.Json.JsonConvert.SerializeObject(_commandArgs);
        }
    }
}
