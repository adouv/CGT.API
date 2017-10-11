using CGT.Api.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Base.Service
{
    /// <summary>
    /// 统一api接口类
    /// </summary>
    interface IService
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        ResponseMessageModel Execute();

    }
}
