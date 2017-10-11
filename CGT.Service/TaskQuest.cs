using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGT.Base.Service.Hangfire;
using CGT.Base.Service.Factoring;
using CGT.Api.DTO;
using System.Linq.Expressions;
using CGT.Base.Service.Insurance;

namespace CGT.Service
{
    public class TaskQuest
    {
        public TaskBaseService TaskBase { get; set; }

        public OverDueMonthlyBillService OverDueMonthlyBill { get; set; }

        public UpdateMonthlylimitCountService updateMonthlylimitCountService { get; set; }

        public TaskQuest()
        {
        }

        public void InitTaskList()
        {
            #region 逾期宽限期利息计算
            //每天0点05分中计算所有账单的逾期宽限期利息，冻结企业状态，计算分销逾期和宽限期次数
            RecurringJob.AddOrUpdate(() => OverDueMonthlyBill.Execute(), Cron.Daily(0,5), TimeZoneInfo.Local);
            //每个月1号0点9分执行保险月限额恢复
            RecurringJob.AddOrUpdate(() => updateMonthlylimitCountService.Execute(), Cron.Monthly(1, 0, 9), TimeZoneInfo.Local);
            #endregion
        }


        public void InitTaskPage()
        {
            //app.Map("/one", r =>
            //{
            //    r.Run(context =>
            //    {
            //        //任务执行一次
            //        BackgroundJob.Enqueue(() => Console.WriteLine($"ASP.NET Core One Start LineZero{DateTime.Now}"));
            //        return context.Response.WriteAsync("ok");
            //    });
            //});
        }

        public void Reset()
        {
            var list = TaskBase.GetJobList();
            foreach (var item in list)
            {
                BackgroundJob.Delete(item.Id.ToString());
            }
        }

    }
}
