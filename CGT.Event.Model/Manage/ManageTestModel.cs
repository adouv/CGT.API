using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Event.Model.Manage
{
    /// <summary>
    /// 差旅导入测试实体
    /// </summary>
    public class ManageTestModel
    {
        /// <summary>
        /// Excel文件流
        /// </summary>
        public byte[] Excel { get; set; }

        /// <summary>
        /// 分销商Code
        /// </summary>
        public string PayCenterCode { get; set; }

        /// <summary>
        /// 分销商名称
        /// </summary>
        public string PayCenterName { get; set; }

        /// <summary>
        /// 分销商商户号
        /// </summary>
        public string MerchantCode { get; set; }
    }
}
