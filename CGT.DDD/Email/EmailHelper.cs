using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace CGT.DDD.Email {
    /// <summary>
    /// 邮件发送类
    /// </summary>
    public class EmailHelper {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailSubjct">邮件主题</param>
        /// <param name="mailBody">邮件正文</param>
        /// <param name="mailAddress">接受邮箱</param>
        /// <param name="fromAddress">发送邮箱</param>
        /// <param name="frommpwd">发送邮箱密码</param>
        /// <param name="fromHost">发送邮箱主机地址</param>
        /// <param name="port">发送邮箱主机端口</param>
        /// <returns></returns>
        public static void Send(string mailSubjct, string mailBody, string mailAddress, string fromAddress, string frommpwd, string fromHost, int port) {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("发送邮件", fromAddress));
            emailMessage.To.Add(new MailboxAddress("mail", mailAddress));
            emailMessage.Subject = mailSubjct;
            emailMessage.Body = new TextPart("plain") { Text = mailBody };

            using (var client = new SmtpClient()) {
                client.Connect(fromHost, port, true);
                client.Authenticate(fromAddress, frommpwd);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="mailSubjct">邮件主题</param>
        /// <param name="mailBody">邮件正文</param>
        /// <param name="mailAddress">接受邮箱</param>
        /// <param name="fromAddress">发送邮箱</param>
        /// <param name="frommpwd">发送邮箱密码</param>
        /// <param name="fromHost">发送邮箱主机地址</param>
        /// <param name="port">发送邮箱主机端口</param>
        /// <returns></returns>
        public static async Task SendEmailAsync(string mailSubjct, string mailBody, string mailAddress, string fromAddress, string frommpwd, string fromHost, int port) {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("发送邮件", fromAddress));
            emailMessage.To.Add(new MailboxAddress("mail", mailAddress));
            emailMessage.Subject = mailSubjct;
            emailMessage.Body = new TextPart("plain") { Text = mailBody };

            using (var client = new SmtpClient()) {
                await client.ConnectAsync(fromHost, port, SecureSocketOptions.None).ConfigureAwait(false);
                await client.AuthenticateAsync(fromAddress, frommpwd);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
