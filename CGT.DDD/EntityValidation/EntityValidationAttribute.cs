using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CGT.DDD.EntityValidation
{
    /// <summary>
    /// 通用验证基类
    /// </summary>
    public class EntityValidationAttribute : ValidationAttribute
    {
        #region Constructors
        public EntityValidationAttribute(MessageType messageId, params object[] args) :
            base(() => MessageManager.Current.GetMessage(messageId, args))
        { }
        #endregion

        #region Protected Properties
        /// <summary>
        /// 字母
        /// </summary>
        protected virtual Regex rLetters { get { return new Regex("[a-zA-Z]{1,}"); } }
        /// <summary>
        /// 验证数字
        /// 子类可以根据自己的逻辑去重写
        /// </summary>
        protected virtual Regex rDigit { get { return new Regex("[0-9]{1,}"); } }
        /// <summary>
        /// 验证邮编
        /// 子类可以根据自己的逻辑去重写
        /// </summary>
        protected virtual Regex rPostNumber { get { return new Regex("^[0-9]{3,14}$"); } }
        /// <summary>
        /// 验证手机
        /// 子类可以根据自己的逻辑去重写
        /// </summary>
        protected virtual Regex rMobile { get { return new Regex(@"^1[3|4|5|6|7|8|9][0-9]\d{4,8}$"); } }
        /// <summary>
        /// 验证电话
        /// 子类可以根据自己的逻辑去重写
        /// </summary>
        protected virtual Regex rTelePhone { get { return new Regex(@"^[0-9]{2,4}-\d{6,8}$"); } }
        /// <summary>
        /// 验证传真
        /// 子类可以根据自己的逻辑去重写
        /// </summary>
        protected virtual Regex rFex { get { return new Regex(@"/^[0-9]{2,4}-\d{6,8}$"); } }
        /// <summary>
        /// 验证Email
        /// 子类可以根据自己的逻辑去重写
        /// </summary>
        protected virtual Regex rEmail { get { return new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); } }
        /// <summary>
        /// 数字或者百分比
        /// </summary>
        protected virtual Regex rNumberAndPercent { get { return new Regex(@"^\d*%?$"); } }
        /// <summary>
        /// 特殊字符
        /// </summary>
        protected virtual Regex rSpecialChar { get { return new Regex(@"^[0-9a-zA-Z\$]+$"); } }
        /// <summary>
        /// 验证小数后2位
        /// </summary>
        protected virtual Regex rcheckMoneyFormat { get { return new Regex(@"^\d+\.\d{2}$"); } }

        #endregion

    }
}
