using System.ComponentModel;

namespace CGT.DDD.Enums
{
    /// <summary>
    /// 枚举
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 公用状态
        /// </summary>
        public enum CommonStatus
        {
            
            [Description("启用")]
            启用 = 0,
            [Description("禁用")]
            禁用 = 1
        }
        /// <summary>
        /// 请求类型
        /// </summary>
        public enum HttpMethod
        {
            /// <summary>
            ///　get方式，以request.querystring传递参数，如URL跳转
            /// </summary>
            Get,
            /// <summary>
            /// post方式，以request.form传递参数，如表单提交
            /// </summary>
            Post,
        }
        /// <summary>
        /// 交易类型
        /// </summary>
        public enum TradeType
        {
            支付 = 0,
            充值 = 1,
            订单还款 = 2,
            账单还款 = 3,
            全部 = 4//客户端交易显示用
        }
        /// <summary>
        /// 交易状态
        /// </summary>
        public enum TradeStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("成功")]
            成功 = 0,
            [Description("失败")]
            失败 = 1,
            [Description("处理中")]
            处理中 = 2
        }
        /// <summary>
        /// 订单支付类型
        /// </summary>
        public enum OrderPayType
        {
            [Description("余额支付")]
            余额支付 = 0,
            [Description("信用占座")]
            信用占座 = 1,
            [Description("充值")]
            充值 = 99
        }
        /// <summary>
        /// 还款状态
        /// </summary>
        public enum RepayStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未还款")]
            未还款 = 0,
            [Description("已还款")]
            已还款 = 1
        }
        /// <summary>
        /// 结算状态
        /// </summary>
        public enum SettlementStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未结算")]
            未结算 = 0,
            [Description("已结算")]
            已结算 = 1
        }
        /// <summary>
        /// 返现状态
        /// </summary>
        public enum BackStatus
        {
            [Description("未返现")]
            未返现 = 0,
            [Description("返现成功")]
            返现成功 = 1,
            [Description("返现失败")]
            返现失败 = 2
        }
        /// <summary>
        /// 挂起状态
        /// </summary>
        public enum TicketCommand
        {
            [Description("退票")]
            Refund,
            [Description("挂起")]
            Suspend,
            [Description("解挂")]
            Unsuspend,
            [Description("校验")]
            Checkinfo
        }
        /// <summary>
        ///接口类型 
        /// </summary>
        public enum InterfaceType
        {
            [Description("机票")]
            Ticket = 0,
            [Description("保险")]
            Insurance = 1
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public enum BusinessType
        {
            [Description("询价")]
            InquiryPrice = 0,
            [Description("支付")]
            Pay = 1,
            [Description("返现")]
            Remoney = 2,
            [Description("账单")]
            Bill = 3,
            [Description("订单还款")]
            OrderPay = 4
        }

        /// <summary>
        /// 回调状态
        /// </summary>
        public enum NodifyStatus
        {
            Success = 0,
            Fail = 1
        }
        /// <summary>
        /// 挂起状态
        /// </summary>
        public enum ApiType
        {
            [Description("支付")]
            Pay,
            [Description("返现")]
            ReMoney,
            [Description("订单还款")]
            OrderRecharge
        }
        /// <summary>
        /// 支付出入帐
        /// </summary>
        public enum PayType
        {
            [Description("入账")]
            入账 = 0,
            [Description("出账")]
            出账 = 1
        }
        /// <summary>
        /// 账单类型
        /// </summary>
        public enum Isrefundment
        {
            [Description("正常交易")]
            正常交易 = 0,
            [Description("退票")]
            退票 = 1,
            [Description("订单还款")]
            订单还款 = 2
        }
        /// <summary>
        /// 挂起状态
        /// </summary>
        public enum suspendedStatus
        {
            [Description("未操作")]
            未操作 = 0,
            [Description("挂起成功")]
            挂起成功 = 1,
            [Description("挂起失败")]
            挂起失败 = 2,
            [Description("解挂成功")]
            解挂成功 = 3,
            [Description("解挂失败")]
            解挂失败 = 4,
        }
        /// <summary>
        ///挂起类型
        /// </summary>
        public enum Category
        {
            [Description("返现")]
            返现 = 0,
            [Description("订单还款")]
            订单还款 = 1,
            [Description("账单还款")]
            账单还款 = 2,
            [Description("未还款")]
            未还款 = 3,

        }
        /// <summary>
        /// 保险退款错误码
        /// </summary>
        public enum bxRefundErrorCode
        {
            [Description("退款成功")]
            退款成功 = 0000,
            [Description("一般性错误")]
            一般性错误 = 4001,
            [Description("参数错误")]
            参数错误 = 4002,
            [Description("合作伙伴错误")]
            合作伙伴错误 = 4003,
            [Description("签名错误")]
            签名错误 = 4004,
            [Description("不支持此服务")]
            不支持此服务 = 4005,
            [Description("错误的纷扰金额")]
            错误的纷扰金额 = 4006,
            [Description("外部提交的交易号重复")]
            外部提交的交易号重复 = 4007,
            [Description("错误字符编码")]
            错误字符编码 = 4008,
            [Description("用户不存在")]
            用户不存在 = 4009,
            [Description("分润绑定关系不存在")]
            分润绑定关系不存在 = 4010,
            [Description("账户状态不允许")]
            账户状态不允许 = 4011,
            [Description("可用余额不足")]
            可用余额不足 = 4012,
            [Description("交易不存在")]
            交易不存在 = 4013,
            [Description("交易状态不允许")]
            交易状态不允许 = 4014,
            [Description("没有足够的退款金额")]
            没有足够的退款金额 = 4015,
            [Description("没有足够的解冻金额")]
            没有足够的解冻金额 = 4016,
            [Description("系统忙")]
            系统忙 = 4017,
            [Description("填写的银行卡信息与用户名不符")]
            填写的银行卡信息与用户名不符 = 4018,
            [Description("余额不足")]
            余额不足 = 4019
        }
        /// <summary>
        /// pnr和票号显示状态
        /// </summary>
        public enum AllowDisplay
        {
            隐藏 = 0,
            显示 = 1
        }



        /// <summary>
        /// 后台用户状态
        /// </summary>
        public enum UserStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("启用")]
            启用 = 0,
            [Description("禁用")]
            禁用 = 1,
            [Description("申请中")]
            申请中 = 2
        }
        public enum InterAccountUserType
        {
            [Description("全部")]
            全部 = -1,
            [Description("采购通")]
            采购通 = 1,
            [Description("支付通道")]
            支付通道 = 2
        }
        /// <summary>
        /// 后台用户类型
        /// </summary>
        public enum BossUserType
        {
            [Description("内部用户")]
            内部用户 = 0,
            [Description("企业用户")]
            企业用户 = 1
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        public enum UserAccountStatus
        {
            [Description("启用")]
            Enable = 0,
            [Description("禁用")]
            Disable = 1,
            [Description("申请中")]
            ApplyMiddle = 2,
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public enum UserType
        {
            [Description("个人用户")]
            Personal = 0
        }


        /// <summary>
        /// 账户类型
        /// </summary>
        public enum BillUserType
        {
            [Description("全部")]
            全部 = -1,
            [Description("采购通个人")]
            采购通个人 = 0,
            [Description("采购通企业")]
            采购通企业 = 1,
            [Description("接口个人")]
            接口个人 = 2,
            [Description("接口企业")]
            接口企业 = 3
        }
        public enum BillType
        {
            [Description("全部")]
            全部 = -1,
            [Description("采购通")]
            采购通 = 0,
            [Description("支付通道")]
            支付通道 = 1
        }
        /// <summary>
        /// 平台类型
        /// </summary>
        public enum PlatformType
        {
            [Description("全部")]
            全部 = -1,
            [Description("不夜城")]
            _BYCheng = 1,
            [Description("Baitour")]
            Baitour = 2,
            [Description("_51Book")]
            _51Book = 4,
            [Description("_8000YI")]
            _8000YI = 8,
            [Description("B2C官网")]
            _B2C = 64,
            [Description("YeeGo")]
            YeeGo = 128,

        }


        /// <summary>
        /// 渠道类型
        /// </summary>
        public enum ChannelType
        {
            [Description("全部")]
            全部 = -1,
            [Description("不夜城")]
            BYCheng = 0,
            [Description("百拓")]
            Baitour = 1,
            [Description("51Book")]
            _51Book = 2,
            [Description("8000YI")]
            _8000YI = 3,
            [Description("易购")]
            YeeGo = 4,
            [Description("B2C官网")]
            B2C = 5,
            [Description("易宝支付")]
            YeeBao = 6,

        }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public enum ReportOrderStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("支付中")]
            支付中 = 0,
            [Description("支付成功")]
            支付成功 = 1,
            [Description("支付失败")]
            支付失败 = 2,
            [Description("出票成功")]
            出票成功 = 3,
            [Description("出票失败")]
            出票失败 = 7


        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public enum PayInterface
        {

            /// <summary>
            /// 支付宝
            /// </summary>
            [Description("支付宝")]
            Alipay,
            /// <summary>
            /// 汇付
            /// </summary>
            [Description("汇付")]
            ChinaPnr,
            /// <summary>
            /// 财付通
            /// </summary>
            [Description("财付通")]
            Tenpay,
            /// <summary>
            /// 快钱
            /// </summary>
            [Description("快钱")]
            _99Bill,
            /// <summary>
            /// 融宝
            /// </summary>
            [Description("融宝")]
            Reapal
        }

        public enum YIPayInterface
        {
            [Description("钱包")]
            Wallet = 1,
            /// <summary>
            /// 支付宝
            /// </summary>
            [Description("支付宝")]
            Alipay = 2,
            /// <summary>
            /// 汇付
            /// </summary>
            [Description("汇付")]
            ChinaPnr = 3,
            /// <summary>
            /// 财付通
            /// </summary>
            [Description("财付通")]
            Tenpay = 4,

        }
        /// <summary>
        /// 行程类型
        /// </summary>
        public enum VoyageType
        {
            /// <summary>
            /// 单程
            /// </summary>
            [Description("单程")]
            OneWay,
            /// <summary>
            /// 往返
            /// </summary>
            [Description("往返")]
            RoundTrip
        }

        /// <summary>
        /// 客票类型
        /// </summary>
        public enum TicketType
        {
            B2B,
            BSP,
            HS
        }
        /// <summary>
        /// 申请单类型
        /// </summary>
        public enum ApplyformType
        {
            /// <summary>
            /// 退票
            /// </summary>
            Refund,
            /// <summary>
            /// 废票
            /// </summary>
            Scrap
        }

        public enum RefundStatus
        {
            /// <summary>
            /// 退票中
            /// </summary>
            Refunding,
            /// <summary>
            /// 退票成功
            /// </summary>
            Success,
            /// <summary>
            /// 退票失败
            /// </summary>
            Failed,
            /// <summary>
            /// 待退票
            /// </summary>
            RefundWait
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 支付中
            /// </summary>
            Paying,
            /// <summary>
            /// 支付成功
            /// </summary>
            PaySuccess,
            /// <summary>
            /// 支付失败
            /// </summary>
            PayFailed,
            /// <summary>
            /// 出票成功
            /// </summary>
            TicketSuccess,

            /// <summary>
            /// 申请退票
            /// </summary>
            Refunding,
            /// <summary>
            /// 退票失败
            /// </summary>
            RefundFailed,
            /// <summary>
            /// 退票成功
            /// </summary>
            RefundSuccess,
            /// <summary>
            /// 出票失败
            /// </summary>
            TicketFailed = 7,

        }

        /// <summary>
        ///期 改状态
        /// </summary>
        public enum AltTicketStatus
        {
            /// <summary>
            /// 待改期
            /// </summary>
            Await,
            /// <summary>
            ///改期中
            /// </summary>
            AltTicketing,
            /// <summary>
            ///改期成功
            /// </summary>
            Success,
            /// <summary>
            ///改期失败
            /// </summary>
            Failed,


        }
        /// <summary>
        /// 验证消息类型
        /// </summary>
        public enum MessageType
        {
            /// <summary>
            /// 为空验证
            /// </summary>
            RequiredField,
            /// <summary>
            /// 大于验证
            /// </summary>
            GreaterThanField,
            /// <summary>
            /// 小于验证
            /// </summary>
            LessThanField,
            /// <summary>
            /// 邮箱验证
            /// </summary>
            EmailField,
            /// <summary>
            /// 数字验证
            /// </summary>
            DigitField,
            /// <summary>
            /// 邮编验证
            /// </summary>
            PostNumberField,
            /// <summary>
            /// 手机验证
            /// </summary>
            MobileField,
            /// <summary>
            /// 电话验证
            /// </summary>
            TelePhoneField,
            /// <summary>
            /// 传真验证
            /// </summary>
            FexField,
            /// <summary>
            /// 特殊字符
            /// </summary>
            SpecialCharField,
            /// <summary>
            /// 身份证
            /// </summary>
            IdCardField,
            /// <summary>
            /// Ip
            /// </summary>
            IpField,
            /// <summary>
            /// 时间戳
            /// </summary>
            TimesTampField,
        }



        /// <summary>
        /// 错误消息
        /// </summary>
        public enum ErrorWhere
        {
            [Description("采购通")]
            CGT = 0,
            [Description("出票失败")]
            PayOrder = 1,
            [Description("退票失败")]
            RefundOrder = 2
        }

        /// <summary>
        /// Boss后台异常订单的处理状态
        /// </summary>
        public enum ProcStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未处理")]
            未处理 = 0,
            [Description("已处理")]
            已处理 = 1,
            [Description("处理中")]
            处理中 = 2
        }

        /// <summary>
        /// 订单PNR授权状态
        /// </summary>
        public enum AuthStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未验证")]
            未验证 = 0,
            [Description("已授权")]
            已授权 = 1,
            [Description("未授权")]
            未授权 = 2
        }

        /// <summary>
        /// 票号状态 0未检查   1正常  2异常
        /// </summary>
        public enum TicketIdStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未验证")]
            未验证 = 0,
            [Description("正常")]
            正常 = 1,
            [Description("异常")]
            异常 = 2
        }


        /// <summary>
        /// 订单退票状态 0未退票  1已退票
        /// </summary>
        public enum RefundTicketStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("无退票")]
            无退票 = 0,
            [Description("有退票")]
            有退票 = 1
        }

        /// <summary>
        /// 订单退款状态 0无退款  1有退款
        /// </summary>
        public enum RefundAliPayStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("无退款")]
            无退款 = 0,
            [Description("有退款")]
            有退款 = 1
        }

        /// <summary>
        /// 列表改期状态
        /// </summary>
        public enum ChangeStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("待改期")]
            待改期 = 0,
            [Description("改期中")]
            改期中 = 1,
            [Description("改期成功")]
            改期成功 = 2,
            [Description("改期失败")]
            改期失败 = 3

        }
        /// <summary>
        /// 详情改期状态
        /// </summary>
        public enum DChangeStatus
        {
            [Description("待改期")]
            待改期 = 0,
            [Description("改期中")]
            改期中 = 1,
            [Description("改期成功")]
            改期成功 = 2,
            [Description("改期失败")]
            改期失败 = 3
        }
        public enum UserAccounUserType
        {
            [Description("全部")]
            全部 = -1,
            [Description("采购通商户")]
            采购通商户 = 1,
            [Description("接口账户")]
            接口账户 = 3
        }
        public enum UserAccountCharStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("启用")]
            启用 = 0,
            [Description("禁用")]
            禁用 = 1
        }
        public enum ABEErrorType
        {
            [Description("全部")]
            全部 = -1,
            [Description("出票")]
            出票 = 0,
            [Description("退票")]
            退票 = 1,
            [Description("授权")]
            授权 = 2
        }
        public enum ABEStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未处理")]
            未处理 = 0,
            [Description("已处理")]
            已处理 = 1,
            [Description("处理中")]
            处理中 = 2
        }
        public enum RefundCharStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("退票中")]
            退票中 = 0,
            [Description("退票成功")]
            退票成功 = 1,
            [Description("退票失败")]
            退票失败 = 2
        }
        public enum TrandBackStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未返现")]
            未返现 = 0,
            [Description("返现成功")]
            返现成功 = 1,
            [Description("返现失败")]
            返现失败 = 2
        }
        public enum APIStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("成功")]
            成功 = 100,
            [Description("失败")]
            失败 = 200
        }
        public enum SUCategory
        {
            [Description("全部")]
            全部 = -1,
            [Description("返现")]
            返现 = 0,
            [Description("订单还款")]
            订单还款 = 1,
            [Description("账单还款")]
            账单还款 = 2
        }
        public enum SUErrorType
        {
            [Description("全部")]
            全部 = -1,
            [Description("全部失败")]
            全部失败 = 0,
            [Description("部分失败")]
            部分失败 = 1
        }
        public enum SUTicketType
        {
            [Description("全部")]
            全部 = -1,
            [Description("B2P")]
            B2P = 0,
            [Description("BSP")]
            BSP = 1
        }
        public enum SUStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("未操作")]
            未操作 = 0,
            [Description("挂起失败")]
            挂起失败 = 2,
            [Description("解挂失败")]
            解挂失败 = 4,
            [Description("人工处理")]
            人工处理 = 5
        }

        /// <summary>
        /// 规则类型
        /// </summary>
        public enum RuleType
        {
            [Description("全部")]
            全部 = -1,
            [Description("预定后几天扫描")]
            预定后几天扫描 = 1,
            [Description("起飞前几天扫描")]
            起飞前几天扫描 = 2,
            [Description("剩余扫描比例")]
            剩余扫描比例 = 3
        }
        public enum ExcTicketType
        {
            [Description("全部")]
            全部 = -1,
            [Description("BSP")]
            BSP = 1,
            [Description("B2B")]
            B2B = 2
        }
        public enum ExceptionType
        {
            [Description("全部")]
            全部 = -1,
            [Description("航班号")]
            航班号 = 1,
            [Description("起飞时间")]
            起飞时间 = 2,
            [Description("客票状态")]
            客票状态 = 3
        }
        public enum ExcStatus
        {
            [Description("全部")]
            全部 = -1,
            [Description("更改航信账户")]
            更改航信账户 = 1,
            [Description("自行解挂")]
            自行解挂 = 2,
            [Description("自行改签")]
            自行改签 = 3,
            [Description("自行退票")]
            自行退票 = 4
        }
        /// <summary>
        /// 风控业务类型
        /// </summary>
        public enum RiskBusinessType
        {
            [Description("机票")]
            机票 = 0
        }
        /// <summary>
        /// 风控短信业务类型
        /// </summary>
        public enum RiskStepType
        {
            [Description("账单短信")]
            账单短信 = 1,
            [Description("提醒还款短信")]
            提醒还款短信 = 2,
            [Description("手动退票短信")]
            手动退票短信 = 3,
            [Description("退票短信")]
            退票短信 = 4,
        }
        /// <summary>
        /// 手机管理类型
        /// </summary>
        public enum RiskManageType
        {
            [Description("注册手机")]
            注册手机 = 0,
            [Description("全员手机")]
            全员手机 = 1,
        }
        public enum BillRefundStatus
        {
            [Description("申请退票中")]
            全部 = -1,
            [Description("申请退票中")]
            申请退票中 = 0,
            [Description("申请退票成功")]
            申请退票成功 = 1,
            [Description("申请退票失败")]
            申请退票失败 = 2
        }

        /// <summary>
        /// 8000Yi交易类型
        /// </summary>
        public enum TranType
        {
            全部 = 0,
            提现 = 1,
            充值 = 2,
            退款 = 3,
            线下充值 = 4,
            支付 = 5,
            //冻结 = 6,
            //分润 = 7,
            //补差 = 8,
            //退分润 = 9,
            //收款 = 10,
            //退多分润 = 11
        }
        /// <summary>
        /// 8000Yi交易类型
        /// </summary>
        public enum TravelRepayType
        {
            未还款 = 0,
            账单还款 = 1,
            提前还款 = 2,
            逾期还款 = 3
        }
    }
}
