using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.DDD.Validation
{
    public class ValidationHelper
    {


        #region 验证身份证
        /// <summary>  
        /// 验证身份证合理性  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public static bool CheckIDCard(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                bool check = CheckIDCard18(idNumber);
                return check;
            }
            else if (idNumber.Length == 15)
            {
                bool check = CheckIDCard15(idNumber);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            y = sum % 11;
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }

        /// <summary>  
        /// 16位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;
        }
        #endregion

        #region 判断对象是否为空
        /// <summary>  
        /// 判断对象是否为空，为空返回true  
        /// </summary>  
        /// <typeparam name="T">要验证的对象的类型</typeparam>  
        /// <param name="data">要验证的对象</param>          
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null  
            if (data == null)
            {
                return true;
            }

            //如果为""  
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull  
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空  
            return false;
        }

        /// <summary>  
        /// 判断对象是否为空，为空返回true  
        /// </summary>  
        /// <param name="data">要验证的对象</param>  
        public static bool IsNullOrEmpty(object data)
        {
            //如果为null  
            if (data == null)
            {
                return true;
            }

            //如果为""  
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull  
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空  
            return false;
        }
        #endregion  

        #region 验证IP地址是否合法
        /// <summary>  
        /// 验证IP地址是否合法  
        /// </summary>  
        /// <param name="ip">要验证的IP地址</param>          
        public static bool CheckIP(string ip)
        {
            //如果为空，认为验证不合格  
            if (IsNullOrEmpty(ip))
            {
                return false;
            }

            //清除要验证字符串中的空格  
            ip = ip.Trim();

            //模式字符串  
            string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

            //验证  
            return RegexHelper.IsMatch(ip, pattern);
        }
        #endregion  

        #region 手机号验证
        /// <summary>
        /// 手机号验证是否合法 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool CheckPhone(string phone)
        {
            //模式字符串  
            string pattern = @"^1[3|4|5|6|7|8|9][0-9]\d{4,8}$";
            //验证  
            return RegexHelper.IsMatch(phone, pattern);
        }
        #endregion

        #region 时间格式验证yyyMddHHmmsss
        /// <summary>
        /// 时间格式验证yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool CheckDateTimeFormat(string datetime)
        {
            //模式字符串  
            //string pattern = @"^\d{4}-(0\d|1[0-2])-([0-2]\d|3[01])( ([01]\d|2[0-3])\:[0-5]\d\:[0-5]\d)$";
            string pattern = @"^\d{13}$";
            //验证  
            return RegexHelper.IsMatch(datetime, pattern);
        }
        #endregion

        #region 特殊字符
        /// <summary>
        /// 验证不能为特殊符号
        /// </summary>
        /// <param name="strNumber">被验证信息</param>
        /// <returns></returns>
        public static bool CheckWholeString(string strNum)
        {
            //模式字符串  
            string pattern = @"^[0-9a-zA-Z\$]+$";
            //验证  
            return RegexHelper.IsMatch(strNum, pattern);
        }
        #endregion

        #region 字母
        /// <summary>
        /// 验证不能为特殊符号
        /// </summary>
        /// <param name="strNumber">被验证信息</param>
        /// <returns></returns>
        public static bool CheckLetterString(string strNum)
        {
            //模式字符串  
            string pattern = @"^[a-zA-Z\$]+$";
            //验证  
            return RegexHelper.IsMatch(strNum, pattern);
        }
        #endregion

        #region 数字
        /// <summary>
        /// 验证不能为特殊符号
        /// </summary>
        /// <param name="strNumber">被验证信息</param>
        /// <returns></returns>
        public static bool CheckNumberString(string strNum)
        {
            //模式字符串  
            string pattern = @"^[0-9\$]+$";
            //验证  
            return RegexHelper.IsMatch(strNum, pattern);
        }
        #endregion
    }
}
