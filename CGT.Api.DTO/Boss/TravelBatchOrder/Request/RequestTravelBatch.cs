using System;
using System.ComponentModel.DataAnnotations;
namespace CGT.Api.DTO.Boss.TravelBatch
{
    public class RequestTravelBatchOrder : RequestBaseModel
    {

        /// <summary>
        /// 批次号
        /// </summary>
        public string TravelBatchId { get; set; }

        /// <summary>
        /// 企业id
        /// </summary>
        public string EnterpriseId { get; set; }


    }
}
