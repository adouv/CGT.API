using CGT.DDD.Encrpty;
using CGT.DDD.Logger;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.CheckTicket.Service
{
    /// <summary>
    /// 工具类 
    /// </summary>
    public class Common
    {
        public NodeEncrpty nodeServices { get; set; }

        public Common()
        {
            IServiceCollection services = new ServiceCollection();
            var path = Directory.GetParent(Directory.GetCurrentDirectory());
            services.AddNodeServices(options =>
            {
                options.ProjectPath = path + @"\NodeEncrpty";
                options.WatchFileExtensions = new[] { ".js" };
            });
            services.AddSingleton<NodeEncrpty>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            nodeServices = serviceProvider.GetService<NodeEncrpty>();
            nodeServices.nodeServices = serviceProvider.GetService<INodeServices>();
        }

        /// <summary>
        /// sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public string Sign(Dictionary<string, object> contents)
        {
            var sortedContents = string.Join("&", from key in contents.Keys
                                                  where key != "sign" && !key.Equals("sign_type")
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return nodeServices.MD5encrypt(sortedContents.Trim('&') + "cgt").Result.ToLower();
        }

        

       
    }

    public class CheckTicketAPI
    {
        /// <summary>
        /// 验证机票信息   1. true  票号正确        Message： 为接口返回的名字
        ///                2. false 票号异常        Message=="0" 票号存在问题
        ///                3. false 接口异常        Message！=“”
        /// </summary>
        /// <param name="TicketView">传入的票信息</param>
        /// <param name="Message">true时为 乘机人姓名，false时为 失败原因</param>
        /// <returns></returns>
        public bool CheckTicket(List<PreRegistrationRequestView> TicketViewList, int PageNum, ref List<TickeNoResult> TicketNoResultList)
        {
            List<TickeNoResult> TicketResultList = new List<TickeNoResult>();
            bool IsOK = false;
            System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            {
                LoggerFactory.Instance.Logger_Info("小何验证总条数为" + TicketViewList.Count, "CheckTicketService");
                #region 设置验证总次数
                int forNum = 0;
                if (TicketViewList.Count < PageNum)
                {
                    forNum = 1;
                }
                else if (TicketViewList.Count % PageNum == 0)
                {
                    forNum = TicketViewList.Count / PageNum;
                }
                else
                {
                    forNum = (TicketViewList.Count / PageNum) + 1;
                }
                #endregion

                #region 循环请求小何

                for (int r = 0; r < forNum; r++)
                {
                    PreRegistrationRequestView[] List;
                    if (forNum == 1)
                    {
                        List = new PreRegistrationRequestView[TicketViewList.Count];
                        TicketViewList.CopyTo(0, List, 0, TicketViewList.Count);
                    }
                    else if (r < forNum - 1)
                    {
                        List = new PreRegistrationRequestView[PageNum];
                        TicketViewList.CopyTo(r * PageNum, List, 0, PageNum);
                    }
                    else
                    {
                        List = new PreRegistrationRequestView[TicketViewList.Count % PageNum];
                        TicketViewList.CopyTo(r * PageNum, List, 0, TicketViewList.Count % PageNum);

                    }
                    for (int rg = 0; rg < 2; rg++)//两次注册
                    {
                        var Result = new PreRegistrationProcessor(List.ToList<PreRegistrationRequestView>()).Execute();//调用注册接口

                        if (Result.Success)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                if (i <= 0)
                                {
                                    if (List.Length <= 10)
                                    {
                                        System.Threading.Thread.CurrentThread.Join(30000);
                                    }
                                    else if (List.Length >= 50)
                                    {
                                        System.Threading.Thread.CurrentThread.Join(300000);
                                    }
                                    else
                                    {
                                        System.Threading.Thread.CurrentThread.Join(60000);
                                    }
                                }
                                else if (i > 0 && i <= 2)
                                {
                                    if (List.Length <= 10)
                                    {
                                        System.Threading.Thread.CurrentThread.Join(30000);
                                    }
                                    else if (List.Length >= 50)
                                    {
                                        System.Threading.Thread.CurrentThread.Join(200000);
                                    }
                                    else
                                    {
                                        System.Threading.Thread.CurrentThread.Join(120000);
                                    }
                                }
                                else
                                {
                                    System.Threading.Thread.CurrentThread.Join(150000);
                                }

                                var CheckResult = new CheckTicketProcessor(Result.Message).ExecuteCheck();
                                if (CheckResult.Success)
                                {
                                    IsOK = true;

                                    TicketResultList.AddRange(getTicketNoResult(CheckResult.Message));
                                    break;
                                }
                                else if (CheckResult.Success == false && CheckResult.Message != "")
                                {

                                    break;
                                }

                            }
                            break;
                        }
                        else if (Result.Message == "")
                        {
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }

                }

                #endregion

            });
            task.Start();

            System.Threading.Tasks.Task ttEnd = task.ContinueWith((ts) =>
            {
                LoggerFactory.Instance.Logger_Info("子线程小何验证总条数为" + TicketViewList.Count, "CheckTicketService");
                #region 设置验证总次数
                int forNum = 0;
                if (TicketViewList.Count < PageNum)
                {
                    forNum = 1;
                }
                else if (TicketViewList.Count % PageNum == 0)
                {
                    forNum = TicketViewList.Count / PageNum;
                }
                else
                {
                    forNum = (TicketViewList.Count / PageNum) + 1;
                }
                #endregion

                #region 循环请求小何

                for (int r = 0; r < forNum; r++)
                {
                    PreRegistrationRequestView[] List;
                    if (forNum == 1)
                    {
                        List = new PreRegistrationRequestView[TicketViewList.Count];
                        TicketViewList.CopyTo(0, List, 0, TicketViewList.Count);
                    }
                    else if (r < forNum - 1)
                    {
                        List = new PreRegistrationRequestView[PageNum];
                        TicketViewList.CopyTo(r * PageNum, List, 0, PageNum);
                    }
                    else
                    {
                        List = new PreRegistrationRequestView[TicketViewList.Count % PageNum];
                        TicketViewList.CopyTo(r * PageNum, List, 0, TicketViewList.Count % PageNum);

                    }
                    for (int rg = 0; rg < 2; rg++)//两次注册
                    {
                        var Result = new PreRegistrationProcessor(List.ToList<PreRegistrationRequestView>()).Execute();//调用注册接口

                        if (Result.Success)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                try
                                {
                                    if (i <= 0)
                                    {
                                        if (List.Length <= 10)
                                        {

                                            System.Threading.Thread.CurrentThread.Join(30000);
                                        }
                                        else if (List.Length >= 50)
                                        {
                                            System.Threading.Thread.CurrentThread.Join(300000);
                                        }
                                        else
                                        {
                                            System.Threading.Thread.CurrentThread.Join(60000);
                                        }
                                    }
                                    else if (i > 0 && i <= 2)
                                    {
                                        if (List.Length <= 10)
                                        {
                                            System.Threading.Thread.CurrentThread.Join(30000);
                                        }
                                        else if (List.Length >= 50)
                                        {
                                            System.Threading.Thread.CurrentThread.Join(200000);
                                        }
                                        else
                                        {
                                            System.Threading.Thread.CurrentThread.Join(120000);
                                        }
                                    }
                                    else
                                    {
                                        System.Threading.Thread.CurrentThread.Join(150000);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LoggerFactory.Instance.Logger_Error(ex, "CheckTicketService");
                                }
                                var CheckResult = new CheckTicketProcessor(Result.Message).ExecuteCheck();
                                if (CheckResult.Success)
                                {
                                    IsOK = true;

                                    TicketResultList.AddRange(getTicketNoResult(CheckResult.Message));
                                    break;
                                }
                                else if (CheckResult.Success == false && CheckResult.Message != "")
                                {

                                    break;
                                }

                            }
                            break;
                        }
                        else if (Result.Message == "")
                        {
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }

                }

                #endregion
            }, TaskContinuationOptions.OnlyOnFaulted);

            try
            {
                task.Wait();
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "CheckTicketService");
                ttEnd.Wait();

            }

            TicketNoResultList = TicketResultList;

            return IsOK;
        }

        /// <summary>
        /// 获取机票验证结果
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public List<TickeNoResult> getTicketNoResult(string response)
        {
            List<TickeNoResult> list = new List<TickeNoResult>();
            var view = JsonConvert.DeserializeObject<ViewBaseCheck>(response);
            foreach (var item in view.checkdata[0].differentlstDetailed)
            {
                TickeNoResult ticket = new TickeNoResult()
                {
                    TicketId = item.ticketno,
                    Name = item.name,
                    TicketPrice = item.fare
                };
                list.Add(ticket);
            }
            return list;
        }
    }
}
