using CGT.Api.DTO;
using CGT.PetaPoco.Repositories.CgtTicket;
using CGT.Entity.CgtTicketModel;
using CGT.Api.DTO.Boss.InterRefund;
using System;
using CGT.DDD.Utils;
using System.Collections.Generic;

namespace CGT.Api.Service.Boss.Refunds
{
    /// <summary>
    ///  国际票退票列表下载
    /// </summary>
    public class DownloadRefundListService : ApiBase<RequestInterRefund>
    {
        #region 注入仓储
        public InterRefundRep interRefundRep { get; set; }
        public ExeclHelper Excel { get; set; }
        #endregion

        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
        }

        protected override void ExecuteMethod()
        {
            var result = interRefundRep.GetInterRefundDownload(new InterRefund
            {
                OrderID = this.Parameter.LocalId,
                OrderOrderId = this.Parameter.OrderId
            });
            List<string> titlelist = new List<string>();;
            titlelist.Add("订单号");
            titlelist.Add("平台订单号");
            titlelist.Add("采购平台");
            titlelist.Add("采购通账号");
            titlelist.Add("订单退款金额");
            titlelist.Add("创建时间");
            titlelist.Add("处理人");
            titlelist.Add("退款状态");
            titlelist.Add("锁单状态");

            List<string> keylist = new List<string>();
            keylist.Add("OrderId");
            keylist.Add("OrderOrderId");
            keylist.Add("Platform");
            keylist.Add("ReapalAccount");
            keylist.Add("Amount");
            keylist.Add("CreateTime");
            keylist.Add("ModifyUserName");
            keylist.Add("Status");
            keylist.Add("AffairStatus");
            
            var filebytes = Excel.ExcelExport<dynamic>(result, titlelist, keylist);
            Result.Data = Convert.ToBase64String(filebytes);
        }
    }
}
