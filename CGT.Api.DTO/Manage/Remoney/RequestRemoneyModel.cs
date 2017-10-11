using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Manage.Remoney {
    /// <summary>
    /// 批量返现请求实体
    /// </summary>
    public class RequestRemoneyModel {
        public string batchNum;//批次号
        public string enterpriseId;//企业编号
        public List<RemoneyOrderMolde> list;//订单状态json数组
    }
    public class RemoneyOrderMolde {
       public string amount;//金额
        public string data;//暂时为空
        public string result_msg;//错误信息
        public string result_code;//错误码
        public bool isSuccess;//true/false
        public string orderId;//订单号
    }
}
