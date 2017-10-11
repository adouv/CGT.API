using System;

namespace CGT.Api.DTO.Boss.User {
    /// <summary>
    /// 获取分销用户列表请求实体
    /// </summary>
    public class RequestGetUserAccountList : RequestBaseModel {
        /// <summary>
        ///用户类型
        /// </summary>
        public int? UserType { get; set; }

        public string PayCenterCode { get; set; }
        public DateTime? ModifyBeginTime { get; set; }

        public DateTime? ModifyEndTime { get; set; }
        public DateTime? CreateBeginTime { get; set; }

        public DateTime? CreateEndTime { get; set; }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
