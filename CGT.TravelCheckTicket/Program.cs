using Autofac;
using CGT.Api.Service.Manage.CheckTicket;
using CGT.DDD.MQ;
using CGT.Event.Model.Manage;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CGT.DDD.Logger;

namespace CGT.TravelCheckTicket
{
    class Program
    {
        [DllImport("User32.dll ", EntryPoint = "FindWindow")]

        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]

        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]

        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        static void Main(string[] args)
        {
            try
            {
                closebtn();
                Console.CancelKeyPress += new ConsoleCancelEventHandler(CloseConsole);
                Console.WriteLine("Begin RabbitMQ");
                //Console.WriteLine("退出请按 Ctrl+C ");
                new RabbitMQClient().ReceiveMessages(check);
                //Console.Read();
                Console.WriteLine("End RabbitMQ");
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Debug("程序异常："+ex.Message, "CGT.TravelCheckTicket");
                
            }
        }

        public static void check(string str)
        {
            ManageRiskModel model = JsonConvert.DeserializeObject<ManageRiskModel>(str);
            new CheckEtermService(model).Execute();
        }

        /// <summary>
        /// 禁用关闭按钮
        /// </summary>
        private static void closebtn()
        {
            //与控制台标题名一样的路径

            string fullPath = System.Environment.CurrentDirectory + "\\CGT.TravelCheckTicket.exe";

            //根据控制台标题找控制台

            int WINDOW_HANDLER = FindWindow(null, fullPath);

            //找关闭按钮

            IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);

            int SC_CLOSE = 0xF060;

            //关闭按钮禁用

            RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);
        }

        /// <summary>  
        /// 关闭时的事件  
        /// </summary>  
        /// <param name="sender">对象</param>  
        /// <param name="e">参数</param>  
        protected static void CloseConsole(object sender, ConsoleCancelEventArgs e)
        {
            Environment.Exit(0);
            //return;
        }
    }
}
