namespace CGT.DDD.EntityValidation
{
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
        /// <summary>
        /// 金钱验证（小数2位）
        /// </summary>
        MoneyField,
    }
}
