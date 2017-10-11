using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.TravelUserFactoring
{
    /// <summary>
    /// 金主列表请求参数
    /// </summary>
    public class RequestTravelUserFactoringListModel : RequestBaseModel
    {
        /// <summary>
        /// 金主名称
        /// </summary>
        public string FactoringName { get; set; }
        /// <summary>
        /// 金主Code
        /// </summary>
        public string FactoringCode { get; set; }
    }
}
