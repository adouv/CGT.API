using System.ComponentModel.DataAnnotations;

namespace CGT.Api.DTO.Boss.Enterprise
{
    public class RequestGetEnterpriseList : RequestBaseModel
    {
        /// <summary>
        ///提交用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID必填")]
        public long UserId { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [Required(ErrorMessage = "当前页码Pageindex必填")]
        public int Pageindex { get; set; }

        /// <summary>
        /// 页面尺寸
        /// </summary>
        [Required(ErrorMessage = "页面尺寸Pagesize必填")]
        public int Pagesize { get; set; }
    }
}
