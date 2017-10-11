namespace CGT.Reapal.Service
{
    /// <summary>
    /// 基类
    /// </summary>
    public class ViewBase {
        /// <summary>
        /// 商户ID
        /// </summary> 
        public string merchant_id { get; set; }
        /// <summary>
        /// 返回码
        /// </summary> 
        public string result_code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary> 
        public string result_msg { get; set; }
    }
    /// <summary>
    /// 开户实体
    /// </summary>
    public class MemberOpenView : ViewBase {
        /// <summary>
        /// 融宝会员号
        /// </summary> 
        public string member_no { get; set; }
    }
    /// <summary>
    /// 绑卡实体
    /// </summary>
    public class MemberBindCardView : ViewBase {
        /// <summary>
        /// 绑卡Id
        /// </summary> 
        public string bind_id { get; set; }
    }
    /// <summary>
    /// 登录实体
    /// </summary>
    public class MemberLoginView : ViewBase {
        /// <summary>
        /// 用户ID
        /// </summary> 
        public string member_id { get; set; }
    }
    /// <summary>
    /// 查询账户余额
    /// </summary>
    public class MemberAccountView : ViewBase {
        /// <summary>
        /// 用户ID
        /// </summary> 
        public string member_id { get; set; }

        /// <summary>
        /// 金额
        /// </summary> 
        public int balance { get; set; }
    }

    /// <summary>
    /// 修改融宝用户
    /// </summary>
    public class ModifyContractInfoView : ViewBase {

    }
    /// <summary>
    /// 充值实体
    /// </summary>
    public class SamePortalView : ViewBase {
        /// <summary>
        /// 融宝交易流水号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 金额
        /// </summary> 
        public string total_fee { get; set; }
        /// <summary>
        /// 状态
        /// </summary> 
        public string status { get; set; }
        /// <summary>
        /// 签名
        /// </summary> 
        public string sign { get; set; }
        /// <summary>
        /// 异步地址id
        /// </summary>
        public string notify_id { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public string fee_amount { get; set; }
    }



    /// <summary>
    /// 充值还款
    /// </summary>
    public class PayEventArgs : SamePortalView {
        /// <summary>
        /// 1充值2订单还款3账单还款
        /// </summary>
        public int PayPattern { get; set; }


        //data:{"trade_no":"10160526002653459",
        //"order_no":"201605263690933067232204",
        //"total_fee":"71056",
        //"status":"TRADE_FINISHED",
        //"sign":"35cf89885feab26e1dbced16a0a44e15",
        //"notify_id":"604e13e2a34e4679894296bfb00f1dab"},
    }

    /// <summary>
    /// 企业转账实体
    /// </summary>
    public class EnterprisePayView : ViewBase {
        /// <summary>
        /// 转账订单号
        /// </summary>
        public string reapal_order_no { get; set; }

    }
    /// <summary>
    /// 代扣实体
    /// </summary>
    public class PaidPayView : ViewBase {
        /// <summary>
        /// 批次号
        /// </summary>
        public string batch_no { get; set; }
    }
    /// <summary>
    /// 返回数据信息
    /// </summary>
    public class PayEventModel {
        public string OrderId { get; set; }

        public bool IsSuccess { get; set; }

        public string Code { get; set; }

        public string Msg { get; set; }

        public string Totalfee { get; set; }
        public string TradeNo { get; set; }

        public string MerchantCode { set; get; }

        /// <summary>
        /// 手续费
        /// </summary>
        public string FeeAmount { get; set; }

    }


}
