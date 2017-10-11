using System;
using System.ComponentModel.DataAnnotations;

namespace CGT.Api.DTO.Boss.Enterprise
{

    public class RequestEnterpriseMonthlyLimit : RequestBaseModel
    {
        /// <summary>
        ///提交用户ID
        /// </summary>
       // [Required(ErrorMessage = "用户ID必填")]
        //public long UserId { get; set; }

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

        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public int? MonthStatue { get; set; }

        public int? EnterpriseWhiteListID { get; set; }


        public int? EnterpriseStatue { get; set; }
        public string PayCenterCode { get; set; }


        public string MerchantNo { get; set; }//金主code,现在就一个金主暂时不用

        public string IsPage{ get; set; } //1 不分页 0或null 分页

    }
}
