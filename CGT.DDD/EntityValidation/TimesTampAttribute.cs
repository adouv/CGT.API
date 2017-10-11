using System;
using CGT.DDD.Validation;

namespace CGT.DDD.EntityValidation
{
    /// <summary>
    /// 时间戳
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class TimesTampAttribute : EntityValidationAttribute
    {
        public TimesTampAttribute(MessageType messageType, params object[] args) :
            base(messageType, args)
        { }
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            else
                return ValidationHelper.CheckDateTimeFormat(value.ToString());
        }
    }
}
