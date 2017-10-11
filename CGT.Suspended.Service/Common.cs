using CGT.DDD.Config;
using CGT.DDD.Encrpty;
using CGT.DDD.Logger;
using CGT.Suspended.Service;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace CGT.SuspendedService {
    public class Common {
        // 传输数据时，使用的编码格式
        private static readonly Encoding MessageEncoding = Encoding.UTF8;

        private static readonly string Des_Key = JsonConfig.JsonRead("deskey");
        private static readonly string Des_Iv = JsonConfig.JsonRead("desiv");
        //private byte[] _key = ASCIIEncoding.ASCII.GetBytes("12345678");
        //private byte[] _iv = ASCIIEncoding.ASCII.GetBytes("12345678");

        //private static readonly ICrypto Cryptor = new DESCrypto();

        //public NodeEncrpty nodeServices { get; set; }
        //public Common() {
        //    IServiceCollection services = new ServiceCollection();
        //    var path = Directory.GetParent(Directory.GetCurrentDirectory());
        //    services.AddNodeServices(options => {
        //        options.ProjectPath = path + @"\NodeEncrpty";
        //        options.WatchFileExtensions = new[] { ".js" };
        //    });
        //    services.AddSingleton<NodeEncrpty>();
        //    IServiceProvider serviceProvider = services.BuildServiceProvider();

        //    nodeServices = serviceProvider.GetService<NodeEncrpty>();
        //    nodeServices.nodeServices = serviceProvider.GetService<INodeServices>();
        //}



        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="client">tcp连接</param>
        /// <param name="response">请求信息</param>
        public static string SendRequest(TcpClient client, string request) {
            try {
                if (!client.Connected)
                    return "";
                // 报文数据加密
                var messageData = MessageEncoding.GetBytes(Encrpty.EncryptDES(request, Des_Key, Des_Iv));
                // 处理报文内容长度标识
                var lengthData = MessageEncoding.GetBytes(NumberPackger.Package(messageData.Length));
                // 获取到基础连接的流； 这里需要特别注意，不能这里用完后，就马上关闭或释放该流
                var stream = client.GetStream();
                // 先发送报文长度标识
                stream.Write(lengthData, 0, lengthData.Length);
                // 发送报文正文内容
                stream.Write(messageData, 0, messageData.Length);
                //byte[] bytes = new byte[1024];
                //int bytesRead = stream.Read(bytes, 0, bytes.Length);
                //return Encoding.ASCII.GetString(bytes, 0, bytesRead);
                return ReceiveResponse(client);
            }
            catch (Exception ex) {
                CGT.DDD.Logger.LoggerFactory.Instance.Logger_Error(ex, "SuspendedServiceErorr");
                return "";
            }
        }

        /// <summary>
        /// 接收请求数据
        /// </summary>
        /// <param name="client">tcp连接</param>
        /// <remarks>
        /// 该方法会一直阻塞，直到接收到数据
        /// </remarks>
        private static string ReceiveResponse(TcpClient client) {
            try {
                while (true) {
                    if (client.Available > 0) {
                        // 先获取报文长度标识
                        var lengthData = ReceiveData(client, NumberPackger.Bits); // 处理报文内容长度标识
                        var messageLength = NumberPackger.Unpackage(lengthData);
                        var ReceiveDataStr = ReceiveData(client, messageLength);
                        // 通过报文长度标识，再获取报文正文内容
                        return Encrpty.DecryptDES(ReceiveDataStr, Des_Key, Des_Iv);
                    }
                }
            }
            catch (Exception ex) {
                CGT.DDD.Logger.LoggerFactory.Instance.Logger_Error(ex, "SuspendedServiceErorr");
            }
            return null;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="client">tcp连接</param>
        /// <param name="length">数据长度</param>
        private static string ReceiveData(TcpClient client, int length) {
            using (var ms = new MemoryStream()) {
                var stream = client.GetStream();
                var receiveTimes = 0;
                while (ms.Length < length) {
                    if (client.Available > 0) {
                        var buffer = new byte[client.Available < length - ms.Length ? client.Available : length - ms.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        ms.Write(buffer, 0, buffer.Length);
                        if (ms.Length == length)
                            break;
                    }
                    else {
                        receiveTimes++;
                        if (receiveTimes > 20)
                            break;
                    }
                }
                return MessageEncoding.GetString(ms.ToArray());
            }
        }
        /// <summary>
        /// 返回错误code值
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        internal static string ReturnCode(string Message) {
            string code = "000";
            switch (Message) {
                case "用户名或密码错误":
                    code = "001";
                    break;
                case "登录失败。":
                    code = "001";
                    break;
                case "执行挂起指令失败.没有找到可用的已连接配置":
                    code = "001";
                    break;
                default:
                    code = "099";
                    break;
            }
            return code;
        }

    }
}
