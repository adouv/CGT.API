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
    /// 订单查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class OrderListProcessor : ProcessorBase<OrderListView>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyCode">商户号</param>
        /// <param name="accountName">返款来源</param>
        public OrderListProcessor(DateTime startDate, DateTime endDate, string departureEndDate, string departureStartDate,
         string companyCode, string companyOrderId, int reStatus,
             string PNR, string parentCode, int isRebate
            )
        {
            _endDate = endDate.ToString("yyyy-MM-dd");
            _startDate = startDate.ToString("yyyy-MM-dd");
            _departureEndDate = departureEndDate;
            _departureStartDate = departureStartDate;
            _companyCode = companyCode;
            _companyOrderId = companyOrderId;
            _reStatus = reStatus.ToString();
            _PNR = PNR;
            _parentCode = parentCode;
            _isRebate = isRebate.ToString();

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
            get { return "OrderAction/orderList"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterBossApiUrl; }
        }


        private readonly string _endDate;
        private readonly string _startDate;

        private readonly string _departureEndDate;
        private readonly string _departureStartDate;
        private readonly string _companyCode;
        private readonly string _companyOrderId;
        private readonly string _reStatus;
        private readonly string _PNR;
        private readonly string _parentCode;
        private readonly string _isRebate;
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("pageNo", pageNo);
            request.Add("pageCount", pageCount);
            request.Add("startDate", _startDate);
            request.Add("endDate", _endDate);
            request.Add("departureEndDate", _departureEndDate);
            request.Add("departureStartDate", _departureStartDate);
            request.Add("companyCode", _companyCode);
            request.Add("companyOrderId", _companyOrderId);
            request.Add("reStatus", _reStatus);
            request.Add("PNR", _PNR);
            request.Add("parentCode", _parentCode);
            request.Add("isRebate", _isRebate);

            return request;
        }
    }
}
