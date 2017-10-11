using System;
using System.Collections.Generic;
using System.Text;
using CGT.DDD.Events;
using CGT.Event.Model;
using CGT.Base.Service.EventHandler.Logger;
using CGT.Api.DTO;
using Newtonsoft.Json;
using CGT.DDD.Logger;

namespace CGT.Base.Service
{
    public abstract class ServiceBase : IService
    {
        static Object locker = new Object();
        /// <summary>
        /// 构造方法
        /// </summary>
        public ServiceBase()
        {
            EventBus.Instance.Subscribe<AddMongoLoggerEvent>(new AddMongoLoggerHandler());
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        public ResponseMessageModel Result { get; set; }

        /// <summary>
        /// 业务实现方法
        /// </summary>
        protected abstract void ExecuteMethod();

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public ResponseMessageModel Execute()
        {
            lock (locker)
            {
                //返回数据加密
                try
                {
                    //执行业务方法
                    this.ExecuteMethod();
                }
                catch (Exception ex)
                {

                    #region 异常处理
                    string code = string.Empty;
                    if (ex.Message.Contains("|"))
                    {
                        code = ex.Message.Split('|')[0].ToString();
                    }
                    else
                    {
                        code = "9999";
                    }
                    this.Result.Data = null;
                    this.Result.ErrorCode = code;
                    this.Result.Message = ex.Message;
                    this.Result.IsSuccess = false;
                    #endregion

                    #region 日志
                    StringBuilder DebugeInfo = new StringBuilder();
                    DebugeInfo.Append("Exception:" + ex.Message + "|" + ex.StackTrace);
                    LoggerFactory.Instance.Logger_Debug(DebugeInfo.ToString(), "ExecuteMethodError");

                    //var addLogger = new AddMongoLoggerEvent() {
                    //    GuidKey = model.GuidKey,
                    //    ActionNmae = "未知",
                    //    Mesasge = ex.Message,
                    //    Code = code,
                    //    StackTrace = code == "9999" ? ex.StackTrace : "",
                    //    CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                    //};
                    //EventBus.Instance.PublishAsync(addLogger);
                    #endregion
                }
                return Result;
            }
        }
    }
}
