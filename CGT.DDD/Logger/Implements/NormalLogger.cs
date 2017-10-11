using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace CGT.DDD.Logger.Implements
{
    /// <summary>
    /// 文本日志
    /// </summary>
    internal class NormalLogger : LoggerBase
    {
        static readonly object objLock = new object();

        protected override string FileUrl
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "_Log");
            }
        }

        protected override void InputLogger(string message, string path = "", string type = "Info")
        {
            string FileUrls = string.Empty;
            string times = DateTime.Now.ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(path)) {
                FileUrls = FileUrl + @"\" + path + @"\" + times;
            }
            else {
                FileUrls = FileUrl + @"\" + times;
            }

            //验证路径
            if (!Directory.Exists(FileUrls))
            {
                Directory.CreateDirectory(FileUrls);
            }

            lock (objLock)//防治多线程读写冲突
            {
                string txtName = string.Empty;
                if (!string.IsNullOrEmpty(type))
                {
                    txtName = type + "_" + times + ".log";
                }
                else
                {
                    txtName = times + ".log";
                }

                string filePath = Path.Combine(FileUrls, txtName);

                using (StreamWriter srFile = File.AppendText(filePath))
                {
                    srFile.WriteLine(string.Format(@"LoggerDateTime:{0}
Type:{3}
ID:{1}
Message:{2}   |||
                        "
                        , DateTime.Now.ToString().PadRight(20)
                        , ("[ThreadID:" + Thread.CurrentThread.ManagedThreadId.ToString() + "]").PadRight(14)
                        , message,path));
                    srFile.Dispose();
                }

            }
        }
    }
}

