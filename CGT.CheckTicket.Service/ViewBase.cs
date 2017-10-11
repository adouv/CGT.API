using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.CheckTicket.Service
{
    /// <summary>
    /// 返回结果基类
    /// </summary>
    public class ViewBase
    {
        /// <summary>
        /// 返回信息代码  200 成功 其他为失败
        /// </summary> 
        public string Err_code { get; set; }
        /// <summary>
        /// 返回提示信息
        /// </summary> 
        public string Err_content { get; set; }
        /// <summary>
        /// 注册唯一编码
        /// </summary>
        public string uuid { get; set; }
    }

    /// <summary>
    /// 注册返回结果
    /// </summary>
    public class ViewBaseRegist
    {
        /// <summary>
        /// 返回基类
        /// </summary>
        public ViewBase ErrorRes { get; set; }
    }


    /// <summary>
    /// 查询验证返回结果
    /// </summary>
    public class ViewBaseCheck
    {
        /// <summary>
        /// 票号数据处理状态数组
        /// </summary>
        public List<Task> task { get; set; }
        /// <summary>
        /// 票号数据验证结果数组
        /// </summary>
        public List<TicketResult> checkdata { get; set; }
        /// <summary>
        /// 返回基类
        /// </summary>
        public ViewBase ErrorRes { get; set; }
    }

    /// <summary>
    /// 票号数据验证结果
    /// </summary>
    public class TicketResult
    {
        /// <summary>
        /// 公司名
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 票号正常数
        /// </summary>
        public int sameTKNum { get; set; }
        /// <summary>
        /// 票号异常数
        /// </summary>
        public int differentTKNum { get; set; }
        /// <summary>
        /// 正常票总金额
        /// </summary>
        public decimal sameTKFare { get; set; }
        /// <summary>
        /// 异常票数总金额
        /// </summary>
        public decimal differentTKFare { get; set; }
        /// <summary>
        /// 总票数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 查询成功总票数
        /// </summary>
        public int success { get; set; }
        /// <summary>
        /// 查询失败总票数
        /// </summary>
        public int fail { get; set; }
        /// <summary>
        /// 正常票信息集合
        /// </summary>
        public List<TicketDetailResult> samelstDetailed { get; set; }
        /// <summary>
        /// 异常票信息集合
        /// </summary>
        public List<TicketDetailResult> differentlstDetailed { get; set; }
    }

    /// <summary>
    /// 票号信息明细
    /// </summary>
    public class TicketDetailResult
    {
        /// <summary>
        /// 乘机人
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string ticketno { get; set; }
        /// <summary>
        /// 出发城市
        /// </summary>
        public string orgcity { get; set; }
        /// <summary>
        /// 到达城市
        /// </summary>
        public string dstcity { get; set; }
        /// <summary>
        /// 航班号
        /// </summary>
        public string flightno { get; set; }
        /// <summary>
        /// 起飞日期
        /// </summary>
        public string flightdate { get; set; }
        /// <summary>
        /// 客票状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Office号
        /// </summary>
        public string officeNo { get; set; }
        /// <summary>
        /// 票面金额
        /// </summary>
        public decimal fare { get; set; }
        /// <summary>
        /// 税费
        /// </summary>
        public decimal tax { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal totalprice { get; set; }
        /// <summary>
        /// 票面类型
        /// </summary>
        public string faretype { get; set; }
        /// <summary>
        /// 税费类型
        /// </summary>
        public string taxtype { get; set; }
    }

    /// <summary>
    /// 票号数据处理状态
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 状态描述
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 状态值
        /// </summary>
        public int iCount { get; set; }
    }
}
