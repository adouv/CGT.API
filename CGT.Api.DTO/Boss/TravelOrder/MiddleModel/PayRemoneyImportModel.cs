using System;
using System.Collections.Generic;

namespace CGT.Api.DTO.Boss.TravelOrder.MiddleModel {
    /// <summary>
    /// 支付返现Excel导入实体
    /// </summary>
    public class PayRemoneyImportModel : RequestBase
    {
        #region 实体属性

        /// <summary>
        /// 支付帐号
        /// </summary>
        public string PayAccount { get; set; }

        /// <summary>
        /// 是否返现 0不返现  1返现
        /// </summary>
        public int IsRemoney { get; set; }

        /// <summary>
        /// 返现账户
        /// </summary>
        public string RemoneyAccount { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 商户Code
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 平台订单号
        /// </summary>
        public string PlateCode { get; set; }

        /// <summary>
        /// 起飞时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 出票时间
        /// </summary>
        public DateTime TicketTime { get; set; }

        /// <summary>
        /// 异步回调地址
        /// </summary>
        public string CallBackUrl { get; set; }

        /// <summary>
        /// 同步地址
        /// </summary>
        public string TimelyUrl { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        public decimal Rebate { get; set; }

        /// <summary>
        /// 定额返点
        /// </summary>
        public decimal SomeRebate { get; set; }

        /// <summary>
        /// PNR
        /// </summary>
        public string PNR { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderPrice { get; set; }

        /// <summary>
        /// 票面价格
        /// </summary>
        public decimal TicketPrice { get; set; }

        /// <summary>
        /// 机建费
        /// </summary>
        public decimal AirFee { get; set; }

        /// <summary>
        /// 燃油费
        /// </summary>
        public decimal FuelFee { get; set; }

        /// <summary>
        /// 出发城市三字码
        /// </summary>
        public string DepartCode { get; set; }

        /// <summary>
        /// 到达城市三字码
        /// </summary>
        public string ArriveCode { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// 到达时间
        /// </summary>
        public DateTime ArriveTime { get; set; }

        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo { get; set; }

        /// <summary>
        /// 航司Code
        /// </summary>
        public string AirCompanyCode { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        public string Cabin { get; set; }

        /// <summary>
        /// Office号
        /// </summary>
        public string OfficeNo { get; set; }

        /// <summary>
        /// 客票类型 0 BSP 1　B2B
        /// </summary>
        public int TicketType { get; set; }

        /// <summary>
        /// 企业编号
        /// </summary>
        public string EnterpriseID { get; set; }

        /// <summary>
        /// 乘机人集合
        /// </summary>
        public Person person;

        #endregion
    }

    /// <summary>
    /// 乘机人实体
    /// </summary>
    public class Person
    {
        /// <summary>
        /// 乘机人姓名
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 乘机人类型 0 成人 1儿童 2婴儿
        /// </summary>
        public int PersonType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 证件类型(0身份证 1护照 2军人证 3台胞证 4港澳通行证 5外国人永久居留证 6旅行证 7回乡证 8其他)
        /// </summary>
        public int CardType { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BrithDay { get; set; }

        /// <summary>
        /// 票号
        /// </summary>
        public string TicketId { get; set; }
    }
}
