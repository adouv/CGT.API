namespace CGT.DDD.EntityValidation
{
    /// <summary>
    /// 验证信息实体
    /// </summary>
    public class RuleViolation
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; private set; }
        /// <summary>
        /// 引起错误的属性
        /// </summary>
        public string PropertyName { get; private set; }

        public RuleViolation(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// 初始化验证信息实体
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="propertyName">引起错误的属性</param>
        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", PropertyName, ErrorMessage);
        }

    }
}
