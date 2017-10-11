using System;

namespace CGT.CheckTicket.Service
{
    

    /// <summary>
    /// 注册传入参数
    /// </summary>
    public class PreRegistrationRequestView
    {
        /// <summary>
        /// 乘机人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string TicketNo { get; set; }
    }

    /// <summary>
    /// 票号验证结果类
    /// </summary>
    public class TickeNoResult
    {
        /// <summary>
        /// 票号
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 票面价
        /// </summary>
        public decimal TicketPrice { get; set; }
    }
}
