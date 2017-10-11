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

namespace CGT.PayCenter.Service {
    /// <summary>
    /// 兜底资金列表
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class FundsRevealListProcessor : ProcessorBase<FundsRevealListView> {
        public FundsRevealListProcessor(DateTime startDate, DateTime endDate, string companyCode, string status) {
            _companyCode = companyCode;
            _status = status;
            _endDate = endDate.ToString("yyyy-MM-dd");
            _startDate = startDate.ToString("yyyy-MM-dd");

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

        protected override bool IsBase {
            get {
                return false;
            }
        }

        protected override string RequestAddress {
            get { return "funds/revealList"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterBossApiUrl; }
        }


        private readonly string _companyCode;
        private readonly string _status;
        private readonly string _endDate;
        private readonly string _startDate;


        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            request.Add("status", _status);
            request.Add("endDate", _endDate);
            request.Add("startDate", _startDate);
            request.Add("pageNo", pageNo);
            request.Add("pageCount", pageCount);
            return request;
        }
    }
}
