using System.Collections.Generic;
using System.Globalization;

namespace CGT.DDD.EntityValidation
{
    /// <summary>
    /// 消息类
    /// [abc] a、b 或 c（简单类） 
    /// [^abc] 任何字符，除了 a、b 或 c（否定） 
    /// [a-zA-Z] a 到 z 或 A 到 Z，两头的字母包括在内（范围）
    /// [a-d[m-p]] a 到 d 或 m 到 p：[a-dm-p]（并集）
    /// [a-z&&[def]] d、e 或 f（交集） 
    /// [a-z&&[^bc]] a 到 z，除了 b 和 c：[ad-z]（减去） 
    /// [a-z&&[^m-p]] a 到 z，而非 m 到 p：[a-lq-z]（减去）
    /// . 任何字符（与行结束符可能匹配也可能不匹配）
    /// \d 数字：[0-9]
    /// \D 非数字： [^0-9]
    /// \s 空白字符：[ \t\n\x0B\f\r]
    /// \S 非空白字符：[^\s]
    /// \w 单词字符：[a-zA-Z_0-9]
    /// \W 非单词字符：[^\w]
    /// </summary>
    public class MessageManager
    {
        static Dictionary<MessageType, string> messages = new Dictionary<MessageType, string>();
        static MessageManager()
        {
            messages.Add(MessageType.RequiredField, "这个 \"{0}\"是必填的!");
            messages.Add(MessageType.GreaterThanField, "这个 \"{0}\" 的值必须大于 \"{1}\"!");
            messages.Add(MessageType.LessThanField, "这个 \"{0}\" 的值必须小于 \"{1}\"!");
            messages.Add(MessageType.EmailField, "这个 \"{0}\" 不是有效的Email地址!");
            messages.Add(MessageType.DigitField, "这个 \"{0}\" 不是有效的数字!");
            messages.Add(MessageType.PostNumberField, "这个 \"{0}\" 不是有效的邮编!");
            messages.Add(MessageType.MobileField, "这个 \"{0}\" 不是有效的手机号码!");
            messages.Add(MessageType.TelePhoneField, "这个 \"{0}\" 不是有效的电话号码!");
            messages.Add(MessageType.FexField, "这个 \"{0}\" 不是有效的传真!");
            messages.Add(MessageType.SpecialCharField, "这个 \"{0}\" 不是有效的名称!");
            messages.Add(MessageType.IdCardField, "这个 \"{0}\" 不是有效的证件!");
            messages.Add(MessageType.IpField, "这个 \"{0}\" 不是有效的IP地址!");
            messages.Add(MessageType.TimesTampField, "这个 \"{0}\" 不是有效的时间戳!");
        }
        /// <summary>
        /// 得到验证异常的消息集合
        /// 对外公开
        /// </summary>
        /// <param name="messageType">异常消息ID</param>
        /// <param name="args">消息参数集合</param>
        /// <returns></returns>
        public string GetMessage(MessageType messageType, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, messages[messageType], args);
        }

        /// <summary>
        /// 本类的实例对象
        /// </summary>
        public static MessageManager Current = new MessageManager();
    }
}
