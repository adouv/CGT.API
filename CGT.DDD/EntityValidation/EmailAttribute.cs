using System;

namespace CGT.DDD.EntityValidation
{
    /// <summary>
    /// Email验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAttribute : EntityValidationAttribute
    {
        public EmailAttribute(MessageType messageType, params object[] args) :
            base(messageType, args)
        { }
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            else
                return rEmail.IsMatch(value.ToString());
        }
    }
}
