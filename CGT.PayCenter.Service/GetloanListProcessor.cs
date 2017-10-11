
using CGT.DDD.Config;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* Getloan
*命名空间* CGT.PayCenter.Service
*创 建 人* tonglei
*创建时间* 2016/11/23 19:56:27
*创建描述* 
*修 改 人* 
*修改时间* 
*修改描述* 

**************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 返款查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 19:56:27
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class GetloanListProcessor : ProcessorBase<GetloanListView>
    {
        public GetloanListProcessor(string startDate, string endDate, string companyCode, string accountName, string companyOrderId, int userCategory, string payCenterCode)
        {
            _companyCode = companyCode;
            _accountName = accountName;
            _companyOrderId = companyOrderId;
            _endDate = endDate;
            _startDate = startDate;
            _userCategory = userCategory;
            _payCenterCode = payCenterCode;
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
            get { return "loanInfo/loanList"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterBossApiUrl; }
        }


        private readonly string _companyCode;
        private readonly string _endDate;
        private readonly string _startDate;
        private readonly string _accountName;
        private readonly string _companyOrderId;
        private readonly string _payCenterCode;
        private readonly int _userCategory;
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            request.Add("endDate", _endDate);
            request.Add("startDate", _startDate);
            request.Add("accountName", _accountName);
            request.Add("companyOrderId", _companyOrderId);
            request.Add("userCategory", _userCategory);
            request.Add("payCenterCode", _payCenterCode);
            request.Add("pageNo", pageNo);
            request.Add("pageCount", pageCount);
            return request;

        }
    }
}
