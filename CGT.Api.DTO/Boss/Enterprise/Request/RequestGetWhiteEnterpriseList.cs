using System.ComponentModel.DataAnnotations;

namespace CGT.Api.DTO.Boss.Enterprise {
    /// <summary>
    /// 获取企业列表请求实体
    /// </summary>
    public class RequestGetWhiteEnterpriseList : RequestBaseModel {
        /// <summary>
        ///商户子编码
        /// </summary>
        public string PayCenterCode { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int? EnterpriseWhiteListID { get; set; }

        public int EnterpriseStatue { get; set; }

        public int FreezeWay { get; set; }
    }
}
