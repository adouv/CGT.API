using CGT.Api.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace CGT.Api.Service
{
    /// <summary>
    /// 统一api接口类
    /// </summary>
    interface IApi
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        ResponseMessageModel Execute();

        /// <summary>
        /// 加入提交参数
        /// </summary>
        /// <param name="json"></param>
        void SetData(RequestModel json);
    }
}
