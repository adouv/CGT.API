using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CGT.DDD.Config;

namespace CGT.DDD.Encrpty {
    public class Encrpty {
        static IBlockCipher engine = new DesEngine();
        /// <summary>
        /// 密码密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5Pwd(string value)
        {
            return MD5Encrypt(value + JsonConfig.JsonRead("Cgtsignkeycgt","UserCenter"));

        }
        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string str) {
            var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "");
        }
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEecrypt(string data, string key) {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(data);
            var aes = Aes.Create();
            aes.Key = keyArray;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            var cipher = aes.CreateEncryptor(UTF8Encoding.UTF8.GetBytes(key), UTF8Encoding.UTF8.GetBytes(key));
            var result_byte = cipher.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            var datastr = Convert.ToBase64String(result_byte);
            return datastr;
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt(string data, string key) {
            var aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            var cipher = aes.CreateDecryptor(UTF8Encoding.UTF8.GetBytes(key), UTF8Encoding.UTF8.GetBytes(key));
            var databyte = Convert.FromBase64String(data);
            var result_byte = cipher.TransformFinalBlock(databyte, 0, databyte.Length);
            var strResult = UTF8Encoding.UTF8.GetString(result_byte);
            return strResult;
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="address"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string RSAEcrypt(string data, string address) {
            byte[] DataToEncrypt = UTF8Encoding.UTF8.GetBytes(data);
            X509Certificate2 cer = new X509Certificate2(address);
            var rsa = cer.GetRSAPublicKey();
            var result_byte = rsa.Encrypt(DataToEncrypt, RSAEncryptionPadding.Pkcs1);
            var strResult = Convert.ToBase64String(result_byte);
            return strResult;
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="address"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string RSADecrypt(string data, string address, string pwd) {
            var strResult = "";
            byte[] data_byte;
            try {
                X509Certificate2 cer = new X509Certificate2(address, pwd, X509KeyStorageFlags.Exportable);
                var rsa = cer.GetRSAPrivateKey();
                data_byte = Convert.FromBase64String(data);
                var result_byte = rsa.Decrypt(data_byte, RSAEncryptionPadding.Pkcs1);
                strResult = UTF8Encoding.UTF8.GetString(result_byte);
            }
            catch (Exception ex) {
                
            }
            return strResult;
        }
        
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">待加密的字符串</param>
        /// <param name="key">加密密钥,要求8位</param>
        /// <param name="iv">偏移向量</param>
        /// <returns></returns>
        public static string EncryptDES(string data, string key, string iv) {
            byte[] byKey = Encoding.ASCII.GetBytes(key);
            byte[] byIV = Encoding.ASCII.GetBytes(iv);
            byte[] byData = Encoding.GetEncoding("gb2312").GetBytes(data);

            BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(engine), new Pkcs7Padding());
            cipher.Init(true, new ParametersWithIV(new DesParameters(byKey), byIV));
            byte[] rv = new byte[cipher.GetOutputSize(byData.Length)];
            int tam = cipher.ProcessBytes(byData, 0, byData.Length, rv, 0);
            cipher.DoFinal(rv, tam);
            return Convert.ToBase64String(rv);
        }
        /// <summary>
        /// 使用DES解密
        /// </summary>
        /// <param name="data">待解密的字符串</param>
        /// <param name="key">解密密钥,要求8位</param>
        /// <param name="vi">偏移向量</param>
        /// <returns></returns>
        public static string DecryptDES(string data, string key, string vi) {
            StringBuilder ret = new StringBuilder();
            foreach (byte b in Convert.FromBase64String(data)) {
                ret.AppendFormat("{0:X2}", b);
            }
            byte[] byData = new byte[ret.ToString().Length / 2];
            for (int x = 0; x < ret.ToString().Length / 2; x++) {
                int i = (Convert.ToInt32(ret.ToString().Substring(x * 2, 2), 16));
                byData[x] = (byte)i;
            }
            byte[] byKey = Encoding.ASCII.GetBytes(key);
            byte[] byVI = Encoding.ASCII.GetBytes(vi);
            BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(engine));
            cipher.Init(false, new ParametersWithIV(new DesParameters(byKey), byVI));
            byte[] rv = new byte[cipher.GetOutputSize(byData.Length)];
            int tam = cipher.ProcessBytes(byData, 0, byData.Length, rv, 0);
            cipher.DoFinal(rv, tam);
            var rvl = new List<byte>();
            rvl.AddRange(rv);
            rvl.RemoveAll(b => b == 0);
            rv = rvl.ToArray();
            return Encoding.GetEncoding("gb2312").GetString(rv);
        }
    }
}
