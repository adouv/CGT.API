using CGT.DDD.Logger;
using CGT.DDD.Utils;
using CGT.Entity.CgtTravelModel;
using CGT.Event.Model.Manage;
using CGT.PetaPoco.Repositories.CgtTravel;
using CGT.Travel.Test.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace CGT.Travel.Test
{
    public class Program
    {


        public static void Main(string[] args)
        {
            Timer timer = new Timer();

            timer.Enabled = true;
            timer.Interval = 60000;//执行间隔时间,单位为毫秒  
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TestRun);
            Console.WriteLine("差旅业务压力测试系统运行中......");
            Console.ReadLine();
        }

        public static void TestRun(object sender, System.Timers.ElapsedEventArgs e)
        {
            //得到 date hour minute  
            string strDate = e.SignalTime.Date.ToString("yyyy-MM-dd");
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            List<Rules_Test> ruleList = new Rules_TestRep().GetRules_TestList();
            foreach (var item in ruleList)
            {
                string Date = Convert.ToDateTime(item.SetTime).Date.ToString("yyyy-MM-dd");
                int Hour = Convert.ToDateTime(item.SetTime).Hour;
                int Minute = Convert.ToDateTime(item.SetTime).Minute;
                if (Date == strDate && Hour == intHour && Minute == intMinute)
                {
                    Running(item);
                }
            }

        }

        public static void Running(Rules_Test rule)
        {
            LoggerFactory.Instance.Logger_Info("开始测试：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "CGT.Travel.Test");
            //总共需要的数据条数
            int TicketNum = Convert.ToInt32(rule.UserNum) * Convert.ToInt32(rule.TicketNum);
            List<BasicData_Test> BasicData_TestList = new BasicData_TestRep().GetBasicData_TestList(TicketNum);

            #region 删除本次数据

            string Ids = "";
            foreach (var item in BasicData_TestList)
            {
                Ids = item.Id + ",";
            }
            if (!string.IsNullOrWhiteSpace(Ids))
            {
                Ids = Ids.Substring(0, Ids.Length - 1);
                new BasicData_TestRep().DeleteBasicData_TestList(Ids);
                LoggerFactory.Instance.Logger_Info("删除数据集合：" + JsonConvert.SerializeObject(BasicData_TestList), "CGT.Travel.Test");
            }

            #endregion

            #region 设置验证总次数

            int forNum = 0;
            if (BasicData_TestList.Count < rule.TicketNum)
            {
                forNum = 1;
            }
            else if (BasicData_TestList.Count % rule.TicketNum == 0)
            {
                forNum = BasicData_TestList.Count / Convert.ToInt32(rule.UserNum);
            }
            else
            {
                forNum = (BasicData_TestList.Count / Convert.ToInt32(rule.UserNum)) + 1;
            }

            #endregion

            #region 循环请求方伟导入

            for (int r = 0; r < forNum; r++)
            {
                #region 导入接口参数组织

                BasicData_Test[] List;
                if (forNum == 1)
                {
                    List = new BasicData_Test[BasicData_TestList.Count];
                    BasicData_TestList.CopyTo(0, List, 0, BasicData_TestList.Count);
                }
                else if (r < forNum - 1)
                {
                    List = new BasicData_Test[Convert.ToInt32(rule.UserNum)];
                    BasicData_TestList.CopyTo(r * Convert.ToInt32(rule.UserNum), List, 0, Convert.ToInt32(rule.UserNum));
                }
                else
                {
                    List = new BasicData_Test[BasicData_TestList.Count % Convert.ToInt32(rule.UserNum)];
                    BasicData_TestList.CopyTo(r * Convert.ToInt32(rule.UserNum), List, 0, BasicData_TestList.Count % Convert.ToInt32(rule.UserNum));

                }

                List<BasicData_Test> datalist = List.ToList<BasicData_Test>();

                List<string> titileList = new List<string>()
                {
                    "票面价","订单金额","起飞日期","出发机场三字码","到达机场三字码","航班号","舱位","乘客姓名","票号","出票日期","PNR","保理企业"
                };

                List<string> keyList = new List<string>()
                {
                    "TicketPrice","OrderAmount","DepartureTime","DepartCode","ArriveCode","FlightNo","Cabin","PersonName","TicketNum","TicketTime","PNR","Enterprise"
                };

                string MerchantCode = "";//融宝商户号
                string PayCenterCode = "";//分销商Code
                string PayCenterName = "";//分销商名称
                List<ManageExcelModel> ExcelList = new List<ManageExcelModel>();
                foreach (var item in datalist)
                {
                    ManageExcelModel excel = new ManageExcelModel()
                    {
                        ArriveCode = item.ArrCode,
                        Cabin = item.Cabin,
                        DepartCode = item.DepCode,
                        DepartureTime = Convert.ToDateTime(item.StratDate),
                        Enterprise = item.EnterpriseName + "|" + item.EnterpriseId.ToString(),
                        FlightNo = item.FlightNo,
                        OrderAmount = Convert.ToDecimal(item.OrderPrice),
                        TicketTime = Convert.ToDateTime(item.TicketDate),
                        TicketPrice = Convert.ToDecimal(item.TicketPrice),
                        TicketNum = item.TicketNo,
                        PNR = item.PNR,
                        PersonName = item.PersonName
                    };
                    ExcelList.Add(excel);
                    PayCenterCode = item.PayCenterCode;
                    PayCenterName = item.PayCenterName;
                    MerchantCode = item.MerchantCode;
                }

                byte[] str = new ExeclHelper(new HostingEnvironment() { WebRootPath="C://" }).ExcelExport<ManageExcelModel>(ExcelList, titileList, keyList);

                #endregion

                //声明一个线程请求导入接口
                System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
                {
                    var Result = new ImportProcessor(str, PayCenterCode, PayCenterName, MerchantCode).Execute();//调用导入接口

                    if (Result.Success)
                    {
                        LoggerFactory.Instance.Logger_Info("导入成功：" + Result.Message, "CGT.Travel.Test");
                    }
                    else
                    {
                        LoggerFactory.Instance.Logger_Info("导入失败：" + Result.Message, "CGT.Travel.Test");
                    }
                });
                task.Start();
            }

            #endregion

        }

    }

    public class HostingEnvironment : IHostingEnvironment
    {

        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
