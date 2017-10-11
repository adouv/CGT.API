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
    /// 订单关联查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class OrderAssociateListProcessor : ProcessorBase<OrderAssociateListView>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyCode">商户号</param>
        /// <param name="accountName">返款来源</param>
        public OrderAssociateListProcessor(DateTime startDate, DateTime endDate, string companyCode,
            string parentCode, string companyOrderId, string reapalNo, int incomeType, string PNR, int userCategory)
        {
            _parentCode = parentCode;
            _companyCode = companyCode;
            _endDate = endDate.ToString("yyyy-MM-dd");
            _startDate = startDate.ToString("yyyy-MM-dd");
            _companyOrderId = companyOrderId;
            _reapalNo = reapalNo;
            _incomeType = incomeType.ToString();
            _PNR = PNR;
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
            get { return "Order/orderAssociateList"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterBossApiUrl; }
        }

        private readonly string _companyCode;
        private readonly string _endDate;
        private readonly string _startDate;

        private readonly string _parentCode;
        private readonly string _reapalNo;
        private readonly string _companyOrderId;
        private readonly string _incomeType;
        private readonly string _PNR;
        private readonly int _userCategory;
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("pageNo", pageNo);
            request.Add("pageCount", pageCount);
            request.Add("startDate", _startDate);
            request.Add("endDate", _endDate);

            request.Add("parentCode", _parentCode);
            request.Add("companyCode", _companyCode);
            request.Add("companyOrderId", _companyOrderId);
            request.Add("reapalNo", _reapalNo);
            request.Add("incomeType", _incomeType);
            request.Add("PNR", _PNR);
            request.Add("userCategory", _userCategory);
            return request;
        }
    }
}
