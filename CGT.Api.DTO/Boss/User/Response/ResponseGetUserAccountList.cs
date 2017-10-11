using System;
using System.Collections.Generic;

namespace CGT.Api.DTO.Boss.User
{
    /// <summary>
    /// 获取分销用户列表响应实体
    /// </summary>
    public class ResponseGetUserAccountList {
        public List<UserInfo> UserInfolist;
    }
    /// <summary>
    /// 分销用户实体
    /// </summary>
    public class UserInfo {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 0启用1禁用2申请中
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 融宝商户编号
        /// </summary>
        public string ReapalMerchantId { get; set; }
        /// <summary>
        /// 商户编码
        /// </summary>
        public string MerchantCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string UserCompanyName { get; set; }
        /// <summary>
        /// 商户子Code
        /// </summary>
        public string PayCenterCode { get; set; }

    }
}
