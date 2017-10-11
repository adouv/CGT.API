namespace CGT.SuspendedService {
    /// <summary>
    /// 基类
    /// </summary>
    public class ViewBase {
        /// <summary>
        /// 请求/执行是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 执行情况描述消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 票号信息
        /// </summary>
        public TicketData ticketData { get; set; }

    }
    /// <summary>
    /// 返回data实体
    /// </summary>
    public class TicketData {
        /// <summary>
        /// 票面价
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 票面价
        /// </summary>
        public string TotalPrice { get; set; }
        /// <summary>
        /// 起飞日期
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 客票状态
        /// </summary>
        public string TicketState { get; set; }
        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 指令参数类
    /// </summary>
    public class CommandArgs {
        /// <summary>
        /// 分类
        /// </summary>
        public string Catelog { get; set; }
        /// <summary>
        /// 客票类型
        /// </summary>
        public string TicketType { get; set; }
        /// <summary>
        /// 指令
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 航司
        /// </summary>
        public string Airline { get; set; }
        /// <summary>
        /// 参数信息
        /// </summary>
        public OperateArgs Args { get; set; }
    }
    /// <summary>
    /// 票号类
    /// </summary>
    public class OperateArgs {
        /// <summary>
        /// 票号
        /// </summary>
        public string TicketNo { get; set; }
        /// <summary>
        /// 乘机人姓名
        /// </summary>
        public string PassengerName { get; set; }
    }
}
