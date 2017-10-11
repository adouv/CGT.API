using CGT.DDD.Config;
using System;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    public class ReapalInsuranceRefundProcessor : ProcessorBase<ViewBase>
    {
        protected override string RequestAddress
        {
            get { return "refund"; }
        }

        protected override string ServiceAddress
        {
            get { return JsonConfig.JsonRead("ReapalH5Url","Reapal"); }
        }

        private string _merchant_id;//商户ID
        private string _orig_order_no;//原订单号
        private string _order_no;//退款单号
        private string _amount;//退款金额
        private string _note;//退款说明
        /// <summary>
        /// 构造方法
        /// </summary>
        public ReapalInsuranceRefundProcessor(string merchant_id, string orig_order_no, string order_no, string amount, string note)
        {
            _merchant_id = merchant_id.Trim();
            _orig_order_no = orig_order_no.Trim();
            _order_no = order_no.Trim();
            _amount = amount;
            _note = note;
        }

        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("merchant_id", _merchant_id);
            result.Add("orig_order_no", _orig_order_no);
            result.Add("order_no", _order_no);
            result.Add("amount", Convert.ToString(Math.Ceiling(Convert.ToDouble(_amount) * 100)));
            result.Add("note", _note);
            return result;
        }


        protected override bool RequireTimeStamp
        {
            get
            {
                return false;
            }
        }
    }
}
