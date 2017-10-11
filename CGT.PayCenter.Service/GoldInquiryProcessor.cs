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
    /// 金主查询接口
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class GoldInquiryProcessor : ProcessorBase<GlodInquiryListView> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyCode">商户号</param>
        /// <param name="accountName">返款来源</param>
        public GoldInquiryProcessor(string companyCode) {
            _companyCode = companyCode;
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
            get { return "advanceInfo/advanceList"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterBossApiUrl; }
        }

        private readonly string _companyCode;
        


        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            return request;
        }
    }
}
