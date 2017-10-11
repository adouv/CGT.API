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

namespace CGT.PayCenter.Service
{
    /// <summary>
    ///当前商户设置的天数查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 19:20:52
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class DaySetOnlySelfListViewProcessor : ProcessorBase<DaySetOnlySelfListView>
    {

        public DaySetOnlySelfListViewProcessor(string companyCode)
        {
            _companyCode = companyCode;
        }


        protected override bool IsBase
        {
            get
            {
                return false;
            }
        }

        protected override string RequestAddress
        {
            get { return "days/dayList"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterBossApiUrl; }
        }

        private readonly string _companyCode;


        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            return request;
        }
    }
}
