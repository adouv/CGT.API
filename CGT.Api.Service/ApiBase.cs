using CGT.Api.DTO;
using CGT.DDD.Encrpty;
using CGT.DDD.IRepositories;
using CGT.DDD.Logger;
using CGT.Entity.CgtModel;
using CGT.Entity.MongoModel;
using CGT.PetaPoco.Repositories.Cgt;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace CGT.Api.Service {
    /// <summary>
    /// api统一抽象基类
    /// </summary>
    /// <typeparam name="P"></typeparam>
    public abstract class ApiBase<P> : IApi
        where P : RequestBaseModel, new() {
        //private static object lockobject = new object();
        //初始化异步锁
        private static SemaphoreSlim _mutex = new SemaphoreSlim(2);
        /// <summary>
        /// 构造方法
        /// </summary>
        public ApiBase() {
            // EventBus.Instance.Subscribe<AddMongoLoggerEvent>(new AddMongoLoggerHandler());
        }

        #region 注入服务
        /// <summary>
        /// 加密服务
        /// </summary>
        public NodeEncrpty Cryptor { get; set; }

        /// <summary>
        /// 用户信息仓储
        /// </summary>
        public InterfaceAccountRep interfaceAccountRep { get; set; }

        /// <summary>
        /// mongodb日志
        /// </summary>
        public IMongoRepository<ApiLoggerMongoModel> mongodbLoggger { get; set; }

        /// <summary>
        /// 返回实体
        /// </summary>
        public ResponseMessageModel Result { get; set; }
        #endregion

        /// <summary>
        /// 请求实体
        /// </summary>
        public RequestModel model { get; private set; }

        /// <summary>
        /// 提交业务参数
        /// 
        /// </summary>
        public P Parameter { get; set; }

        /// <summary>
        /// 业务实现方法
        /// </summary>
        protected abstract void ExecuteMethod();

       
        /// <summary>
        /// 赋值提交的实体数据
        /// </summary>
        /// <param name="json"></param>
        public virtual void SetData(RequestModel json) {
            model = json;
        }

    
        /// <summary>
        /// 验证
        /// </summary>
        protected virtual void Validate() {
            //lock (lockobject) {
            var interfaceAccount = new InterfaceAccount() {
                MerchantCode = model.MerchantId
            };
            var _interfaceAccount = interfaceAccountRep.GetInterfaceAccount(interfaceAccount);
            //获取userRSA地址
            var aesAddress = _interfaceAccount.CertAddress.Split('|')[1];
            string AesKey = "";
            try {
                AesKey = Encrpty.RSADecrypt(model.EncryptKey, aesAddress, _interfaceAccount.CertPassword);
            }
            catch (Exception ex) {
                LoggerFactory.Instance.Logger_Debug(model.EncryptKey + "|" + aesAddress + "|" + _interfaceAccount.CertPassword + "|" + ex.Message, "RSADecryptError");
                var exstr = new RSADecryptException();
                throw new RSADecryptException(Common.ServiceCommon.GetExMessage(exstr.ErrorCode, "RSA解密失败！"));
            }
            //解密data
            var json = Encrpty.AESDecrypt(model.Data, AesKey);

            // 反序列化Json为参数对象
            this.Parameter = JsonConvert.DeserializeObject<P>(json);
            string MySign = GetMySign(_interfaceAccount.UserKey);

            //验证sign
            if (!this.Parameter.Sign.Equals(MySign)) {
                var ex = new ApiSignException();
                throw new ApiSignException(Common.ServiceCommon.GetExMessage(ex.ErrorCode, "签名验证失败！"));
            }

            //验证数据 
            if (!this.Parameter.IsValid) {
                var ex = new ValidationException();
                throw new ValidationException(Common.ServiceCommon.GetExMessage(ex.ErrorCode, this.Parameter.GetRuleViolationMessages()));
            }
            //}
        }

        /// <summary>
        /// 获取MySign
        /// </summary>
        private string GetMySign(string userkey) {
            //MySign =(MerchantId = 12345 & TimesTamp = 2017 - 01 - 25 10:21:49 & Ip=167.0.12.31 & MAC = aaaa)+UserKey的值
            string MySign = Encrpty.MD5Encrypt(string.Format(@"MerchantId={0}&TimesTamp={1}&Ip={2}&Mac={3}{4}"
                        , model.MerchantId
                        , this.Parameter.TimesTamp
                        , this.Parameter.Ip
                        , this.Parameter.Mac
                        , userkey));

            return MySign;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public ResponseMessageModel Execute() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try {
                //执行验证
                if (model!=null)
                {
                    this.Validate();
                }
                
                //异步锁等待
                _mutex.WaitAsync();
                //执行业务方法
                this.ExecuteMethod();
            }
            catch (Exception ex) {
                LoggerFactory.Instance.Logger_Error(ex, "ExecuteMethodError");
                #region 异常处理
                string code = string.Empty;
                if (ex.Message.Contains("|")) {
                    code = ex.Message.Split('|')[0].ToString();
                }
                else {
                    code = "9999";
                }
                this.Result.Data = null;
                this.Result.ErrorCode = code;
                this.Result.Message = ex.Message;
                //this.Result.MerchantId = model.MerchantId;
                this.Result.IsSuccess = false;
                #endregion

                #region 日志
                StringBuilder DebugeInfo = new StringBuilder();
                //DebugeInfo.Append("Model:" + JsonConvert.SerializeObject(this.model) + "\r\n");
                DebugeInfo.Append("Parameter:" + JsonConvert.SerializeObject(this.Parameter) + "\r\n");
                DebugeInfo.Append("Exception:" + ex.Message + "|" + ex.StackTrace);
                LoggerFactory.Instance.Logger_Debug(DebugeInfo.ToString(), "ExecuteMethodError");

                //var addLogger = new AddMongoLoggerEvent() {
                //    GuidKey = model.GuidKey,
                //    ActionNmae = "未知",
                //    Mesasge = ex.Message,
                //    Code = code,
                //    StackTrace = code == "9999" ? ex.StackTrace : "",
                //    CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                //};
                //EventBus.Instance.PublishAsync(addLogger);
                #endregion
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            LoggerFactory.Instance.Logger_Info(string.Format("请求花费{0}ms", ts.TotalMilliseconds), "ApiReQuestTime");
            return Result;
        }
    }
}
