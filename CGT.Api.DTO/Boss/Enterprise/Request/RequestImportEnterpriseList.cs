using System.ComponentModel.DataAnnotations;

namespace CGT.Api.DTO.Boss.Enterprise
{
    public class RequestImportEnterpriseList : RequestBaseModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID为必填项")]
        public long UserId { get; set; }
    }
}
