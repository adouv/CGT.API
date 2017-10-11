using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Manage.CheckTicket
{
    public abstract class CheckTicketBaseService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckTicketBaseService() { }

        /// <summary>
        /// 验证
        /// </summary>
        public virtual void Validate()
        {
        }
        /// <summary>
        /// 执行
        /// </summary>
        public abstract void Execute();

    }
}
