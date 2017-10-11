using CGT.Entity.CgtModel;
using System;
using System.IO;
using CGT.DDD.Certificate;
using CGT.DDD.Config;

namespace CGT.Api.Service.Common
{
    /// <summary>
    /// 公共服务
    /// </summary>
    public static class ServiceCommon
    {
        /// <summary>
        /// 拼接异常消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetExMessage(string code, string message)
        {
            return string.Format("{0}|{1}", code, message);
        }

        /// <summary>
        /// 通过用户接口编号生成证书
        /// </summary>
        /// <returns></returns>
        public static InterfaceAccount GenerateUserCer(InterfaceAccount model)
        {
            #region 获取配置
            Random ran = new Random();
            string certificateName = JsonConfig.JsonRead("cerName", "UserCenter") + model.ID + "_" + ran.Next(10, 1000) + "_" + DateTime.Now.Second; ;
            string certificateTool = JsonConfig.JsonRead("cerTools", "UserCenter");
            string certificateAdd = JsonConfig.JsonRead("cerAdderss", "UserCenter") + model.MerchantName + @"\";
            string certificatePass = JsonConfig.JsonRead("cerPassword", "UserCenter");
            #endregion

            #region 生成证书
            if (!Directory.Exists(certificateAdd))
            {
                Directory.CreateDirectory(certificateAdd);
            }
            certificateAdd += certificateName;
            CertificateHelper.CreateCertWithPrivateKey(certificateName, certificateTool);
            CertificateHelper.ExportToCerFile(certificateName, certificateAdd + ".cer");//公钥
            CertificateHelper.ExportToPfxFile(certificateName, certificateAdd + ".pfx", certificatePass, true);//私钥 
            #endregion
            model.CertAddress = certificateAdd + ".cer|" + certificateAdd + ".pfx";
            model.CertPassword = certificatePass;
            return model;
        }
    }
}
