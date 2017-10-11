using CGT.DDD.Logger;
using CGT.DDD.SOA;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CGT.SuspendedService {

    /// <summary>
    /// 客票挂起基类
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ProcessorBase<TResponse, TResult> {
        // 数据加解密
        //private static readonly ICrypto Cryptor = new DESCrypto();

        public Common com { get; set; }

        public ProcessorBase() {
            com = new Common();
        }


        /// <summary>
        /// 执行调用
        /// </summary>
        /// <returns></returns>
        public ExecResult<TResult> Execute() {
            var result = new ExecResult<TResult>();
            //参数初始化
            var targets = string.Empty;
            var request = string.Empty;
            var response = string.Empty;
            DateTime? reqTime = null;
            DateTime? resTime = null;
            //获取地址
            targets = GetRequestUrl();
            //组织参数
            request = PrepareRequest();
            //分割IP和端口号
            var target = targets.Split(':');
            TcpClient cline = new TcpClient();

            try {
                //与服务器连接
                cline.Connect(target[0], int.Parse(target[1]));
                cline.ReceiveTimeout = 200000;
                cline.SendTimeout = 200000;
                reqTime = DateTime.Now;
                //请求服务器
                response = Common.SendRequest(cline, request);
                resTime = DateTime.Now;
                result = ParseResponse(response);
                cline.Dispose();
            }
            catch (Exception ex) {
                LoggerFactory.Instance.Logger_Error(ex);
                result = new ExecResult<TResult> {
                    Success = false,
                    Message = ex.Message
                };
            }
            if (reqTime.HasValue) {
                LoggerFactory.Instance.Logger_Info(
                    string.Format("SuspendedService----target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                        Environment.NewLine, target, reqTime, request, resTime, response), "CGT.Suspended.Service");
            }

            cline.Dispose();

            return result;
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseResponse(string response) {
            var result = new ExecResult<TResult>();
            if (typeof(TResponse) == typeof(TResult)) {
                var sData = JsonConvert.DeserializeObject<ResponseView>(response);
                if (sData.Success) {
                    result.Success = true;
                    result.MsgCode = "000";
                }
                else {
                    result.Success = false;
                    result.Message = sData.Message;
                    result.MsgCode = Common.ReturnCode(sData.Message);
                }
                result.Result = JsonConvert.DeserializeObject<TResult>(response);
            }
            else {
                result.Result = ParseResponseCore(JsonConvert.DeserializeObject<TResponse>(response));
            }
            return result;
        }
        /// <summary>
        /// 接口地址
        /// </summary>
        protected abstract string ServiceAddress { get; }
        /// <summary>
        /// 请求参数
        /// </summary>
        /// <returns></returns>
        protected abstract string PrepareRequestCore();

        protected virtual TResult ParseResponseCore(TResponse response) {
            throw new InvalidOperationException();
        }

        #region 方法
        /// <summary>
        /// 获取请求url
        /// </summary>
        /// <returns></returns>
        private string GetRequestUrl() {
            return ServiceAddress;
        }


        private string PrepareRequest() {
            var parameters = PrepareRequestCore();
            return parameters;
        }
        #endregion

        #region 返回信息实体
        /// <summary>
        /// 返回实体类
        /// </summary>
        class ResponseView {


            public bool Success { get; set; }

            public string Message { get; set; }

        }
        #endregion
    }
    public abstract class ProcessorBase<T> : ProcessorBase<T, T> {
    }
}
