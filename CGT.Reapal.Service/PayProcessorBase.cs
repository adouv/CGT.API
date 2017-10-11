using CGT.DDD.Config;
using CGT.DDD.Enums;
using CGT.DDD.Logger;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 在线充值委托
    /// </summary>
    /// <param name="e">数据源</param>
    public delegate void OnlineEventHandler(PayEventArgs e);

    /// <summary>
    /// 支付基类
    /// </summary>
    public abstract class PayProcessorBase
    {
        public Common com { get; set; }

        public static readonly string ReapalMerchantId = JsonConfig.JsonRead("ReapalMerchantId", "Reapal");
        public PayProcessorBase()
        {
            com = new Common();
        }

        //网关地址
        protected abstract string Gateway { get; }
        //同步地址
        protected abstract string ReturnAddress { get; }
        //异步地址
        protected abstract string NotifyAddress { get; }

        /// <summary>
        /// 请求参数
        /// </summary>
        /// <returns></returns>
        protected abstract SortedDictionary<string, string> PrepareRequestCore();

        /// <summary>
        /// 回调处理
        /// </summary>
        public abstract PayEventModel Notify_Return(EnumHelper.HttpMethod httpMethod);

        #region Events
        /// <summary>
        /// 成功
        /// </summary>
        public event OnlineEventHandler Success;
        /// <summary>
        /// 失败
        /// </summary>
        public event OnlineEventHandler Fail;
        /// <summary>
        /// 成功时触发
        /// </summary>
        /// <param name="e"></param>
        protected void OnSuccess(PayEventArgs e)
        {
            if (Success != null)
                Success(e);
        }
        /// <summary>
        /// 失败时触发
        /// </summary>
        /// <param name="e"></param>
        protected void OnFail(PayEventArgs e)
        {
            if (Fail != null)
                Fail(e);
        }
        #endregion

        /// <summary>
        /// 执行操作form提交数据
        /// </summary>
        /// <param name="userKey">空的时候默认配置中的userkey</param>
        /// <returns></returns>
        public string ExecuteForm(string userKey = "")
        {
            var dic = PrepareRequestCore();
            string signText = "";
            if (string.IsNullOrEmpty(userKey))
            {
                signText = com.Sign(dic);
            }
            else
            {
                signText = com.Sign(dic, userKey);
            }
            dic.Add("sign", signText);
            //序列化json
            string json = JsonConvert.SerializeObject(dic);

            //加密业务数据--用AES对称加密算法
            string AESKey = com.nodeServices.GenerateAESKey();
            string strData = com.nodeServices.AESencrypt(json, AESKey).Result;

            //加密AESKey--用RSA非对称加密算法
            string strKey = com.nodeServices.RSAencrypt(AESKey, JsonConfig.JsonRead("ReapalPublicKeyCerUrl","Reapal")).Result;

            string data = strData;
            string encryptkey = strKey;
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='reapapaysubmit' action='" + Gateway + "' method='post'>");
            sbHtml.Append("<input type='hidden' value='" + data + "' name='data' />");
            sbHtml.Append("<input type='hidden' value='" + encryptkey + "' name='encryptkey'/>");
            sbHtml.Append("<input type='hidden' value='" + dic["merchant_id"] + "' name='merchant_id' />");
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='btnReapal' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['reapapaysubmit'].submit();</script>");
            LoggerFactory.Instance.Logger_Info(string.Format(@"ReapalRecharge----提交参数:{0},提交加密参数:{1}", json, sbHtml.ToString()));
            return sbHtml.ToString();
        }




    }
}
