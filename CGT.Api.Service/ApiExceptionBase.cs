using CGT.DDD.Logger;
using System;

namespace CGT.Api.Service
{
    /// <summary>
    /// 自定义错误基类
    /// </summary>
    public abstract class ApiExceptionBase : Exception
    {
        /// <summary>
        /// 获取异常错误码
        /// </summary>
        /// <value>错误码</value>
        public abstract string ErrorCode { get; }


        public ApiExceptionBase() : base() { }

        /// <summary>
        /// 使用指定的错误消息初始化 <see cref="ApiExceptionBase" /> 类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public ApiExceptionBase(string action, string message) : base(message)
        {
            //TODO:记录异常日志
        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="ApiExceptionBase" /> 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public ApiExceptionBase(string action, string message, Exception innerException) : base(message, innerException)
        {
            //TODO:记录异常日志
        }
    }

    /// <summary>
    /// 数据验证异常
    /// </summary>
    public class ValidationException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0001";
            }
        }

        public ValidationException() : base("ValidationException", "数据验证异常！") { }

        public ValidationException(string message) : base("ValidationException", message) { }

        public ValidationException(string message, Exception innerException) : base("ValidationException", message, innerException) { }
    }

    /// <summary>
    /// 商户异常
    /// </summary>
    public class MerchantException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0002";
            }
        }

        public MerchantException() : base("MerchantException", "商户信息异常！") { }

        public MerchantException(string message) : base("MerchantException", message) { }

        public MerchantException(string message, Exception innerException) : base("MerchantException", message, innerException) { }
    }

    /// <summary>
    /// api签名异常
    /// </summary>
    public class ApiSignException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0003";
            }
        }

        public ApiSignException() : base("ApiSignException", "api签名异常！") { }

        public ApiSignException(string message) : base("ApiSignException", message) { }

        public ApiSignException(string message, Exception innerException) : base("ApiSignException", message, innerException) { }
    }

    /// <summary>
    /// 明亮异常
    /// </summary>
    public class PayCenterException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0004";
            }
        }
        public PayCenterException() : base("PayCenterException", "内部调用失败！") { }
        public PayCenterException(string message) : base("PayCenterException", message) { }
        public PayCenterException(string message, Exception innerException) : base("PayCenterException", message, innerException) { }

    }
    /// <summary>
    /// 平台异常
    /// </summary>
    public class PlatformException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0005";
            }
        }
        public PlatformException() : base("PlatformException", "内部调用失败！") { }
        public PlatformException(string message) : base("PlatformException", message) { }
        public PlatformException(string message, Exception innerException) : base("PlatformException", message, innerException) { }

    }

    /// <summary>
    /// 下单异常
    /// </summary>
    public class PlaceOrderException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0006";
            }
        }
        public PlaceOrderException() : base("PlaceOrderException", "内部调用失败！") { }
        public PlaceOrderException(string message) : base("PlaceOrderException", message) { }
        public PlaceOrderException(string message, Exception innerException) : base("PlaceOrderException", message, innerException) { }

    }
    /// <summary>
    /// 支付异常
    /// </summary>
    public class PayException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0007";
            }
        }
        public PayException() : base("PayException", "内部调用失败！") { }
        public PayException(string message) : base("PayException", message) { }
        public PayException(string message, Exception innerException) : base("PayException", message, innerException) { }

    }


    /// <summary>
    /// 融宝异常
    /// </summary>
    public class ReapalApiException : ApiExceptionBase
    {
        public override string ErrorCode
        {
            get
            {
                return "0008";
            }
        }

        public ReapalApiException() : base("ReapalApiException", "融宝异常！") { }

        public ReapalApiException(string message) : base("ReapalApiException", message) { }

        public ReapalApiException(string message, Exception innerException) : base("ReapalApiException", message, innerException) { }
    }
    /// <summary>
    /// RSA解密异常
    /// </summary>
    public class RSADecryptException : ApiExceptionBase {
        public override string ErrorCode {
            get {
                return "0009";
            }
        }

        public RSADecryptException() : base("RSADecryptException", "RSA解密异常！") { }

        public RSADecryptException(string message) : base("RSADecryptException", message) { }

        public RSADecryptException(string message, Exception innerException) : base("RSADecryptException", message, innerException) {
            LoggerFactory.Instance.Logger_Debug(message, "RSADecryptError");
        }
    }

}
