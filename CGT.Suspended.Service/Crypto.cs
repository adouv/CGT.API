using CGT.DDD.Config;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CGT.Suspended.Service {
    interface ICrypto {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content">内容</param>
        string Encrypt(string content);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">内容</param>
        string Decrypt(string content);
    }

    /// <summary>
    /// 明文 加解密处理类
    /// </summary>
    class DefaultCrypto : ICrypto {
        public string Encrypt(string content) {
            return content;
        }

        public string Decrypt(string content) {
            return content;
        }
    }

    /// <summary>
    /// DES 加解密处理类
    /// </summary>
    class DESCrypto : ICrypto {
        //private byte[] _key = ASCIIEncoding.ASCII.GetBytes(JsonConfig.JsonRead("deskey"));
        //private byte[] _iv = ASCIIEncoding.ASCII.GetBytes(JsonConfig.JsonRead("desiv"));
        private byte[] _key = ASCIIEncoding.ASCII.GetBytes("12345678");
        private byte[] _iv = ASCIIEncoding.ASCII.GetBytes("12345678");


        public string Encrypt(string content) {
            var data = Encoding.Default.GetBytes(content);

            using (var stream = new MemoryStream()) {
                var cryptoProvider = new DESCryptoServiceProvider();
                using (var crypto = new CryptoStream(stream, cryptoProvider.CreateEncryptor(_key, _iv), CryptoStreamMode.Write)) {
                    crypto.Write(data, 0, data.Length);
                    crypto.FlushFinalBlock();
                    return Convert.ToBase64String(stream.ToArray(), 0, (int)stream.Length);
                }
            }
        }

        public string Decrypt(string content) {
            var data = Convert.FromBase64String(content);

            using (var stream = new MemoryStream()) {
                var cryptoProvider = new DESCryptoServiceProvider();
                using (var crypto = new CryptoStream(stream, cryptoProvider.CreateDecryptor(_key, _iv), CryptoStreamMode.Write)) {
                    crypto.Write(data, 0, data.Length);
                    crypto.FlushFinalBlock();
                    return Encoding.Default.GetString(stream.ToArray());
                }
            }
        }
    }
}
