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
    /// 利息管理下载
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class InterestInfoExportListProcessor : ProcessorBase<InterestInfoExportListView>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyCode">商户号</param>
        /// <param name="accountName">返款来源</param>
        public InterestInfoExportListProcessor(DateTime startDate, DateTime endDate, string companyCode)
        {
            _companyCode = companyCode;
            _endDate = endDate.ToString("yyyy-MM-dd");
            _startDate = startDate.ToString("yyyy-MM-dd");

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
            get { return "interestInfo/getExportInterest"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }

        private readonly string _companyCode;
        private readonly string _endDate;
        private readonly string _startDate;


        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("startDate", _startDate);
            request.Add("endDate", _endDate);
            request.Add("companyCode", _companyCode);
            return request;
        }
    }
}
