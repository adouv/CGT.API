namespace CGT.Api.DTO.Boss.InterRefund
{
    /// <summary>
    /// 国际票退票提交请求实体
    /// </summary>
    public class RequestSubmitInterRefund : RequestBaseModel
    {
        /// <summary>
        /// 本地订单号
        /// </summary>
        public string LocalId { get; set; }
        //public int ModifyUserId { get; set; }

        //public string ModifyUserName { get; set; }
        public int UserId { get; set; }
        public string Remark { get; set; }
    }
}
