using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace CGT.DDD.Logger.Implements
{
    /// <summary>
    /// 日志核心基类
    /// 模版方法模式，对InputLogger开放，对其它日志逻辑隐藏，InputLogger可以有多种实现
    /// </summary>
    internal abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// 日志文件地址 
        /// </summary>
        protected virtual string FileUrl
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "_Log");
            }
        }

        /// <summary>
        /// 日志持久化的方法，派生类必须要实现自己的方式
        /// </summary>
        /// <param name="message"></param>
        protected abstract void InputLogger(string message, string path, string type);

        #region ILogger 成员

        public void Logger_Timer(string message, Action action,string path)
        {
            StringBuilder str = new StringBuilder();
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            str.Append(message);
            action();
            str.Append("Logger_Timer:代码段运行时间(" + sw.ElapsedMilliseconds + "毫秒)");
            InputLogger(str.ToString(), path, "Logger_Timer");
            sw.Stop();
        }

        public void Logger_Exception(string message, Action action, string path)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                InputLogger("Logger_Exception:" + message + "代码段出现异常,信息为" + ex.Message, path, "Logger_Exception");
            }
        }

        public virtual void Logger_Info(string message, string path)
        {
            InputLogger("Info:" + message, path, "Info");
        }

        public virtual void Logger_Error(Exception ex, string path)
        {
            InputLogger("Error:" + ex.Message + "|" + ex.StackTrace, path, "Error");
        }

        public virtual void Logger_Debug(string message, string path)
        {
            InputLogger("Debug:" + message, path, "Debug");
        }

        public virtual void Logger_Fatal(string message, string path)
        {
            InputLogger("Fatal:" + message, path, "Fatal");
        }

        public virtual void Logger_Warn(string message, string path)
        {
            InputLogger("Warn" + message, path, "Warn");
        }

        #endregion
    }
}
