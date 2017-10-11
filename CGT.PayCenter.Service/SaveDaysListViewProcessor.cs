using CGT.DDD.Config;
using System;
using System.Collections.Generic;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* interestList
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
    /// 当前商户天数设置保存
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 19:20:52
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class SaveDaysListViewProcessor : ProcessorBase<SaveDaysListView> {
        private readonly string _companyCode;
        private readonly string _originHtml;
        private readonly string _loginName;
        private readonly List<int> _indexList;
        private readonly List<DaySetHistory> _list;

        public SaveDaysListViewProcessor(string companyCode, string originHtml, string loginName, List<DaySetHistory> list, List<int> indexList) {
            _companyCode = companyCode;
            _originHtml = originHtml;
            _loginName = loginName;
            _indexList = indexList;
            _list = list;
        }


        protected override bool IsBase {
            get {
                return false;
            }
        }

        protected override string RequestAddress {
            get { return "days/savedays"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterBossApiUrl; }
        }
        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            request.Add("originHtml", _originHtml);
            request.Add("loginName", _loginName);
            request.Add("indexList", _indexList);
            request.Add("list", _list);
            return request;
        }
    }


}
