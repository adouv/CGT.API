using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Event.Model.Manage {
    /// <summary>
    /// 导入风控-针对批次表
    /// </summary>
    public class ManageRiskModel {
        /// <summary>
        /// 商户Code
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 分销商名称
        /// </summary>
        public string PayCenterName { get; set; }
        /// <summary>
        /// 保理编号
        /// </summary>
        public int UserFactoringId { get; set; }
        /// <summary>
        /// 保理名称
        /// </summary>
        public string FactoringName { get; set; }
        /// <summary>
        /// 保理账号
        /// </summary>
        public string FactoringEmail { get; set; }
        /// <summary>
        /// 保理商户号
        /// </summary>
        public string FactoringReapalNo { get; set; }
        /// <summary>
        /// 保理费率
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// 分销账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 分销商户号【ReapalMerchantId】
        /// </summary>
        public string BackReapalNo { get; set; }
        /// <summary>
        /// 分销费率
        /// </summary>
        public decimal FactoringInterestRate { get; set; }
        /// <summary>
        /// 账期天数
        /// </summary>
        public string AccountPeriod { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string TravelBatchId { get; set; }


        /// <summary>
        /// 企业编号
        /// </summary>
        public long EnterpriseID { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        ///基础风控信息集合
        /// </summary>
        public List<BaseRiskModel> baseRiskModelList { get; set; }
    }

    /// <summary>
    /// 针对批次订单表
    /// </summary>
    public class BaseRiskModel {

        #region Excel基本信息

        /// <summary>
        /// 票面价
        /// </summary>
        public decimal TicketPrice { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 起飞时间
        /// </summary>
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// 出发机场三字码
        /// </summary>
        public string DepartCode { get; set; }
        /// <summary>
        /// 到达机场三字码
        /// </summary>
        public string ArriveCode { get; set; }
        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo { get; set; }
        /// <summary>
        /// 舱位
        /// </summary>
        public string Cabin { get; set; }
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string TicketNum { get; set; }
        /// <summary>
        /// 出票日期
        /// </summary>
        public DateTime TicketTime { get; set; }
        /// <summary>
        /// PNR
        /// </summary>
        public string PNR { get; set; }
        /// <summary>
        /// 企业编号
        /// </summary>
        public long EnterpriseID { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        #endregion


        /// <summary>
        /// 商户Code
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 分销商名称
        /// </summary>
        public string PayCenterName { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string TravelBatchId { get; set; }
        /// <summary>
        ///风控类型 0黑屏 1员工白名单 2黑屏+白名单
        /// </summary>
        public int TravelRiskType { get; set; }
        /// <summary>
        ///黑屏验证类型 0验证小何  1黑屏   2先小何后黑屏 (-1 测试验证纯白名单) 
        /// </summary>
        public int EtermType { get; set; }
        /// <summary>
        ///白名单验证结果  0失败 1成功
        /// </summary>
        public int WhiteResultState { get; set; }
        /// <summary>
        ///黑屏验证结果  0失败 1成功
        /// </summary>
        public int BlackResultState { get; set; }
        /// <summary>
        /// 小何批次号
        /// </summary>
        public string UUId { get; set; }
        /// <summary>
        /// 小何注册状态 0 失败 1成功
        /// </summary>
        public int RegisterStatus { get; set; }
        /// <summary>
        /// 小何接口调用状态 0 失败 1 成功
        /// </summary>
        public int CheckStatus { get; set; }
        /// <summary>
        /// 黑屏接口调用状态 0失败 1成功
        /// </summary>
        public int EtermStatus { get; set; }
        /// <summary>
        /// 黑屏成功比率
        /// </summary>
        public decimal EtermSuccessRate { get; set; }
        /// <summary>
        /// 黑屏失败比率
        /// </summary>
        public decimal EtermFailRate { get; set; }
        /// <summary>
        /// 白名单成功比率
        /// </summary>
        public decimal WhiteSuccessRate { get; set; }
        /// <summary>
        /// 白名单失败比率
        /// </summary>
        public decimal WhiteFailRate { get; set; }
        /// <summary>
        /// 验证失败原因
        /// </summary>
        public string FailReason { get; set; }
    }
}
