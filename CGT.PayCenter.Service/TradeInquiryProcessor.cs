using CGT.DDD.Config;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* FundsRevealList
*命名空间* CGT.PayCenter.Service
*创 建 人* tonglei
*创建时间* 2016/11/23 16:53:02
*创建描述* 
*修 改 人* 
*修改时间* 
*修改描述* 

**************************************************************/
using System;
using System.Collections.Generic;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 交易接口查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class TradeInquiryProcessor : ProcessorBase<TradeInquiryListView>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate">返款开始日期</param>
        /// <param name="endDate">返款结束日期</param>
        /// <param name="incomeType">交易状态 0支付  1还款  2返现</param>
        /// <param name="companyOrderId">订单号</param>
        /// <param name="repalNo">交易号</param>
        /// <param name="parentCode">商户号</param>
        public TradeInquiryProcessor(string startDate, string endDate, int incomeType, string companyOrderId, string repalNo, string parentCode, int userCategory)
        {
            _incomeType = incomeType.ToString();
            _companyOrderId = companyOrderId;
            _repalNo = repalNo;
            _parentCode = parentCode;
            _endDate = endDate;
            _startDate = startDate;
            _userCategory = userCategory;
            this.pageCount = 10;
            this.pageNo = 1;
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int pageNo { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageCount { get; set; }
         
        protected override bool IsBase
        {
            get
            {
                return false;
            }
        }

        protected override string RequestAddress
        {
            get { return "tradeInfo/tradeList"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterBossApiUrl; }
        }

        private readonly string _incomeType;
        private readonly string _companyOrderId;
        private readonly string _repalNo;
        private readonly string _parentCode;
        private readonly string _endDate;
        private readonly string _startDate;
        private readonly int _userCategory;


        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("incomeType", _incomeType);
            request.Add("companyOrderId", _companyOrderId);
            request.Add("repalNo", _repalNo);
            request.Add("parentCode", _parentCode);
            request.Add("endDate", _endDate);
            request.Add("startDate", _startDate);
            request.Add("userCategory", _userCategory);
            request.Add("pageNo", pageNo);
            request.Add("pageCount", pageCount);
            return request;
        }
    }
}
