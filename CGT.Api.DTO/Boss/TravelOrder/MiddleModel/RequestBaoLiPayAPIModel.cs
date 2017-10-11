using CGT.DDD.EntityValidation;
using CGT.DDD.SOA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.Api.DTO.Boss.TravelOrder.MiddleModel {
    public class RequestBaoLiPayAPIModel : RequestBase
    {
        /// <summary>
        ///返现账号
        /// </summary>
        //[EmailAttribute(MessageType.EmailField, null, ErrorMessage = "不是有效的返现帐号!")]
        public string UserName { get; set; }
        /// <summary>
        ///支付账号
        /// </summary>
        [EmailAttribute(MessageType.EmailField, null, ErrorMessage = "不是有效的支付帐号!")]
        public string PayUserName { get; set; }
        /// <summary>
        ///是否返现
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string IsRemoney { get; set; }
        /// <summary>
        /// 平台订单号
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string OrderId { get; set; }

        /// <summary>
        /// 商户code
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// 起飞时间
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string StartDate { get; set; }

        /// <summary>
        /// 出票时间
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string TicketTime { get; set; }

        /// <summary>
        /// 异步回调地址
        /// </summary>

        public string NotifyUrl { get; set; }

        /// <summary>
        /// 同步回调地址
        /// </summary> 
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        public int Rebate { get; set; }

        /// <summary>
        /// 定额返点
        /// </summary>
        public int RetMoney { get; set; }

        /// <summary>
        /// Pnr
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string Pnr { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        
        public string OrderPrice { get; set; }
        /// <summary>
        /// 票面价
        /// </summary>
        
        public string TicketPrice { get; set; }

        /// <summary>
        /// 机建费
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public int AirPortTax { get; set; }

        /// <summary>
        /// 燃油费
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public int FuelTax { get; set; }

        ///乘机人信息
        public TravelPassenger Passenger { get; set; }

        ///航段信息
        public Voyage Voyage { get; set; }

        /// <summary>
        ///企业编号
        /// </summary>
        
        public int EnterpriseID { get; set; }

        
    }
    /// <summary>
    /// 乘机人
    /// </summary>
    public class TravelPassenger
    {
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string PassengerName { get; set; }
        /// <summary>
        /// 乘客类型 0成人 1儿童 
        /// </summary>
        public int PassengerType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string CertificateNumber { get; set; }
        /// <summary>
        /// 证件类型 0身份证 1护照 2军人证 3台胞证 4港澳通行证 5外国人永久居留证 6旅行证 7回乡证 8其他
        /// </summary>
        public int CertificateType { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string AirTicketNo { get; set; }
        /// <summary>
        /// 生日
        /// </summary> 
        public string Birthday { get; set; }
    }
    /// <summary>
    /// 航段
    /// </summary>
    public class Voyage {
        /// <summary>
        /// 出发机场(三字码)
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string Departure { get; set; }

        /// <summary>
        /// 到达机场(三字码)
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string Arrival { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string DepartureTime { get; set; }

        /// <summary>
        /// 到达时间
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string ArrivalTime { get; set; }

        /// <summary>
        /// 舱位
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string Bunk { get; set; }

        /// <summary>
        /// 航空公司
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string Airline { get; set; }

        /// <summary>
        /// 航班号
        /// </summary>
        [Required(ErrorMessage = "必须填写")]
        public string FlightNo { get; set; }

    }
}
