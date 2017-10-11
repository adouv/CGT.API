using System;
using CGT.DDD.Config;
using CGT.DDD.Logger.Implements;

namespace CGT.DDD.Logger
{
    /// <summary>
    /// 日志生产类
    /// Singleton模式和策略模式和工厂模式
    /// </summary>
    public sealed class LoggerFactory : ILogger
    {

        #region Logger有多种实现时,需要使用Singleton模式
        /// <summary>
        /// 对外不能创建类的实例
        /// </summary>
        private LoggerFactory()
        {
            string type = "file";
            if (!string.IsNullOrEmpty(JsonConfig.JsonRead("LoggerType", "Logging")))
            {
                type = JsonConfig.JsonRead("LoggerType", "Logging").ToLower();
            }

            switch (type)
            {
                case "file":
                    iLogger = new NormalLogger();
                    break;
                default:
                    throw new ArgumentException("请正确配置AppSetting的LoggerType节点（file）");
            }
        }

        /// <summary>
        /// 日志级别
        /// </summary>
        private static Level level = (Level)Enum.Parse(typeof(Level), JsonConfig.JsonRead("LoggerLevel", "Logging").ToUpper());
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object lockObj = new object();
        /// <summary>
        /// 日志工厂
        /// </summary>
        private static LoggerFactory instance;
        /// <summary>
        /// 日志提供者，只在本类中实例化
        /// </summary>
        private ILogger iLogger;
        /// <summary>
        /// 单例模式的日志工厂对象
        /// </summary>
        public static LoggerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new LoggerFactory();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region ILogger 成员
        public void Logger_Timer(string message, Action action,string path = "")
        {
            iLogger.Logger_Timer(message, action, path);
        }
        public void Logger_Exception(string message, Action action, string path = "")
        {
            iLogger.Logger_Exception(message, action, path);
        }
        public void Logger_Debug(string message, string path = "")
        {
            if (level <= Level.DEBUG)
                iLogger.Logger_Debug(message, path);
        }
        public void Logger_Info(string message, string path = "")
        {
            if (level <= Level.INFO)
                iLogger.Logger_Info(message, path);
        }
        public void Logger_Warn(string message, string path = "")
        {
            if (level <= Level.WARN)
                iLogger.Logger_Warn(message, path);
        }
        public void Logger_Error(Exception ex, string path = "")
        {
            if (level <= Level.ERROR)
                iLogger.Logger_Error(ex, path);
        }
        public void Logger_Fatal(string message, string path = "")
        {
            if (level <= Level.FATAL)
                iLogger.Logger_Fatal(message, path);
        }
        #endregion


    }
}
