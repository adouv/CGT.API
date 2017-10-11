using CGT.DDD.Config;
using System;
using System.Collections.Generic;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* 
*命名空间* CGT.PayCenter.Service
*创 建 人* tonglei
*创建时间* 2016/11/23 19:20:52
*创建描述* 
*修 改 人* 
*修改时间* 
*修改描述* 

**************************************************************/

namespace CGT.PayCenter.Service {
    /// <summary>
    ///金主垫资信息接口查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 19:20:52
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class GoldElectronicInfoListViewProcessor : ProcessorBase<GoldElectronicInfoListView> {

        public GoldElectronicInfoListViewProcessor(string companyCode, string accountCode, string accountType) {
            _companyCode = companyCode;
            _accountCode = accountCode;
            _accountType = accountType;
            this.pageCount = 10;
            this.pageNo = 1;
        }


        protected override bool IsBase {
            get {
                return false;
            }
        }

        protected override string RequestAddress {
            get { return "advanceInfo/advanceInfoList"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterBossApiUrl; }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int pageNo { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageCount { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        private readonly string _companyCode;
        /// <summary>
        /// 关联号
        /// </summary>
        private readonly string _accountCode;
        /// <summary>
        /// 商户类型
        /// </summary>
        private readonly string _accountType;
        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            request.Add("accountCode", _accountCode);
            request.Add("accountType", _accountType);
            request.Add("pageNo", pageNo);
            request.Add("pageCount", pageCount);
            return request;
        }
    }
}
