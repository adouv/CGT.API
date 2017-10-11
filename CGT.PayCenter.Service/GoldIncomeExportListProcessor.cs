using CGT.DDD.Config;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* 
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
    /// 入金查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class GoldIncomeInquiryListProcessor : ProcessorBase<GoldIncomeInquiryListView>
    {
        private readonly string _startDate;
        private readonly string _endDate;
        private readonly string _companyCode;
        private readonly string _companyOrderId;
        private readonly string _reapalNo;
        private readonly string _payName;
        private readonly int _incomeType;
        private readonly decimal _maxAmt;
        private readonly decimal _minAmt;
        private readonly string _accountName;
        private readonly string _companyName;
        private readonly string _stepNo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyCode">商户CODE</param>
        /// <param name="companyOrderId">商户订单号</param>
        /// <param name="reapalNo">融宝交易号</param>
        /// <param name="payName">付款账户名</param>
        /// <param name="expenditureType">出金类型</param>
        /// <param name="maxAmt">最大出金金额</param>
        /// <param name="minAmt">最小出金金额</param>
        /// <param name="accountName">账户名称</param>
        /// <param name="companyName">商户名称</param>
        /// <param name="stepNo">String	步骤编号</param>
        /// <param name="isDownload"></param>
        public GoldIncomeInquiryListProcessor(
             DateTime startDate, DateTime endDate, string companyCode, string companyOrderId, string reapalNo, string payName, int incomeType, decimal maxAmt, decimal minAmt, string accountName, string companyName, string stepNo)
        {
            _startDate = startDate.ToString("yyyy-MM-dd");
            _endDate = endDate.ToString("yyyy-MM-dd"); ;
            _companyCode = companyCode;
            _companyOrderId = companyOrderId;
            _reapalNo = reapalNo;
            _payName = payName;
            _incomeType = incomeType;
            _maxAmt = maxAmt;
            _minAmt = minAmt;
            _accountName = accountName;
            _companyName = companyName;
            _stepNo = stepNo;
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
            get { return "IncomeGolden/incomeList"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterBossApiUrl; }
        }


        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();

            request.Add("pageNo", pageNo);
            request.Add("pageCount", pageCount);
            request.Add("startDate", _startDate);
            request.Add("endDate", _endDate);
            request.Add("companyCode", _companyCode);
            request.Add("companyOrderId", _companyOrderId);
            request.Add("reapalNo", _reapalNo);
            request.Add("payName", _payName);
            request.Add("incomeType", _incomeType);
            request.Add("maxAmt", _maxAmt);
            request.Add("minAmt", _minAmt);
            request.Add("accountName", _accountName);
            request.Add("companyName", _companyName);
            request.Add("stepNo", _stepNo);
            return request;
        }
    }
}
