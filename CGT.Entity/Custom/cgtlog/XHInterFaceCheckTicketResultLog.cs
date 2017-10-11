using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Entity.Custom.cgtlog
{
    /// <summary>
    /// 小和接口日志类
    /// </summary>
    public class XHInterFaceCheckTicketResultLog
    {
        /// <summary>
        /// Id主键
        /// </summary>
        [ResultColumn]
        public long Id { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [ResultColumn]
        public string BatchNumber { get; set; }

        /// <summary>
        /// 注册结果：0 失败  1成功
        /// </summary>
        [ResultColumn]
        public int RegisterStatus { get; set; }

        /// <summary>
        /// 验证结果：0 验证超时  1 验证成功
        /// </summary>
        [ResultColumn]
        public int CheckStatus { get; set; }

        /// <summary>
        /// 票数
        /// </summary>
        [ResultColumn]
        public string TicketNum { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [ResultColumn]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 验证时间
        /// </summary>
        [ResultColumn]
        public DateTime CheckTime { get; set; }
    }
}
