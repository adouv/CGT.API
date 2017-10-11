using CGT.DDD.Config;
using CGT.DDD.Logger;
using CGT.DDD.SOA;
using CGT.DDD.Utils.Http;
using CGT.Entity.CgtLogModel;
using CGT.PetaPoco.Repositories.CgtLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.CheckTicket.Service
{
    /// <summary>
    /// 接口基类
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ProcessorBase<TResponse, TResult>
    {
        //设置请求参数数据格式
        private const string RequestEncodingName = "UTF-8";
        private const string ParameterEncodingName = "UTF-8";

        //
        //public static readonly string PreRegistrationApiUrl = JsonConfig.JsonRead("CheckTicket", "PreRegistrationApiUrl");
        public readonly string CheckTicketApiUrl = JsonConfig.JsonRead("CheckTicketApiUrl", "CheckTicket");
        public readonly string CheckTicketHashcode = JsonConfig.JsonRead("CheckTicketHashcode", "CheckTicket");
        public readonly string CheckTicketKey = JsonConfig.JsonRead("CheckTicketKey", "CheckTicket");
        public readonly string CheckTicketNotifyURL = JsonConfig.JsonRead("CheckTicketNotifyURL", "CheckTicket");

        public Common com { get; set; }

        public ProcessorBase()
        {
            com = new Common();
            
        }

        /// <summary>
        /// 接口名称
        /// </summary>
        protected abstract string RequestAddress { get; }
        /// <summary>
        /// 接口地址
        /// </summary>
        protected abstract string ServiceAddress { get; }
        /// <summary>
        /// 
        /// </summary>
        protected virtual bool IsBase
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 请求参数
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, object> PrepareRequestCore();
        protected virtual TResult ParseResponseCore(TResponse response)
        {
            throw new InvalidOperationException();
        }
        /// <summary>
        /// 执行调用api(注册)
        /// </summary>
        /// <returns></returns>
        /// 
        public ExecResult<TResult> Execute()
        {
            var result = new ExecResult<TResult>();
            var target = string.Empty;
            var request = string.Empty;
            var response = string.Empty;
            DateTime? reqTime = null;
            DateTime? resTime = null;
            try
            {
                target = GetRequestUrl();
                request = PrepareRequest();
                reqTime = DateTime.Now;
                response = HttpRequest.HttpRequestUtility.SendPostRequestjson(target, request, RequestEncodingName, 60000, null, null, "application/json");
                resTime = DateTime.Now;


                if (IsBase)
                {
                    result = ParseResponse(response);
                }
                else
                {
                    result = ParseCenterResponse(response);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "CheckTicketService");
                result = new ExecResult<TResult>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            if (reqTime.HasValue)
            {
                LoggerFactory.Instance.Logger_Debug(
                    string.Format("CheckTicketService----target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                        Environment.NewLine, target, reqTime, request, resTime, response), "CheckTicketService");
              
            }
            return result;
        }
        /// <summary>
        /// 返回信息（注册）
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseResponse(string response)
        {
            var result = new ExecResult<TResult>();
            var view = JsonConvert.DeserializeObject<ViewBaseRegist>(response);

            if (typeof(TResponse) == typeof(TResult))
            {
                if (view.ErrorRes.Err_code == "200" && view.ErrorRes.uuid != "0" && view.ErrorRes.uuid != "")
                {
                    result.Success = true;
                    result.Message = view.ErrorRes.uuid;
                    result.MsgCode = view.ErrorRes.Err_code;

                    #region 添加小和注册成功日志
                    XHInterFaceCheckTicketResultLog log = new XHInterFaceCheckTicketResultLog()
                    {
                        RegisterStatus = 1,
                        BatchNumber = view.ErrorRes.uuid,
                        AddTime = DateTime.Now,
                        CheckStatus = 0,
                        CheckTime = DateTime.Now,
                        TicketNum = view.ErrorRes.Err_content.Substring(4, view.ErrorRes.Err_content.IndexOf(',') - 5)
                    };
                    new XHInterFaceCheckTicketResultLogRep().AddXHInterFaceCheckTicketResultLog(log);
                    #endregion
                }
                else
                {
                    result.Success = false;
                    result.Message = view.ErrorRes.Err_content;

                    #region 添加小和注册失败日志
                    XHInterFaceCheckTicketResultLog log = new XHInterFaceCheckTicketResultLog()
                    {
                        RegisterStatus = 0,
                        BatchNumber = "",
                        AddTime = DateTime.Now,
                        CheckStatus = 0,
                        CheckTime = DateTime.Now,
                        TicketNum = "0"
                    };
                    new XHInterFaceCheckTicketResultLogRep().AddXHInterFaceCheckTicketResultLog(log);
                    #endregion
                }

            }
            else
            {
                // result.Result = ParseResponseCore(JsonConvert.DeserializeObject<TResponse>(view));
            }
            return result;
        }
        /// <summary>
        /// 返回信息（注册）
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseCenterResponse(string response)
        {
            var result = new ExecResult<TResult>();
            var view = JsonConvert.DeserializeObject<ViewBaseRegist>(response);
            if (view != null)
            {
                result.MsgCode = "0000";
                result.Message = "成功";
                result.Success = true;

            }
            else
            {
                result.MsgCode = "0001";
                result.Message = "失败";
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// 执行调用api(验证结果)
        /// </summary>
        /// <returns></returns>
        /// 
        public ExecResult<TResult> ExecuteCheck()
        {
            var result = new ExecResult<TResult>();
            var target = string.Empty;
            var request = string.Empty;
            var response = string.Empty;
            DateTime? reqTime = null;
            DateTime? resTime = null;
            try
            {
                target = GetRequestUrl();
                reqTime = DateTime.Now;
                response = HttpRequest.HttpRequestUtility.SendGetRequest(target, RequestEncodingName, 60000);
                resTime = DateTime.Now;


                if (IsBase)
                {
                    result = ParseResponseCheck(response);
                }
                else
                {
                    result = ParseCenterResponseCheck(response);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "CheckTicketService");
                result = new ExecResult<TResult>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            if (reqTime.HasValue)
            {
                LoggerFactory.Instance.Logger_Info(
                    string.Format("CheckTicketService----target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                        Environment.NewLine, target, reqTime, request, resTime, response), "CheckTicketService");
            }
            return result;
        }
        /// <summary>
        /// 返回信息（验证结果）
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseResponseCheck(string response)
        {
            var result = new ExecResult<TResult>();
            ViewBaseCheck view = new ViewBaseCheck();

            try
            {

                view = JsonConvert.DeserializeObject<ViewBaseCheck>(response);
                if (typeof(TResponse) == typeof(TResult))
                {
                    if (view.ErrorRes.Err_code == "200" && view.task[0].status.Trim() == "已完成" && view.task[0].iCount > 0 && view.checkdata[0].success > 0)
                    {
                        result.Success = true;
                        if (view.checkdata[0].differentTKNum > 0)
                        {
                            result.Message = response;

                        }
                        result.MsgCode = view.ErrorRes.Err_code;
                        //添加小和验证成功日志
                        new XHInterFaceCheckTicketResultLogRep().UpdateXHInterFaceCheckTicketResultLog(view.ErrorRes.uuid, 1);
                    }
                    else if (view.ErrorRes.Err_code == "200" && view.task[0].status.Trim() == "已完成" && view.task[0].iCount > 0 && view.checkdata[0].success == 0)
                    {
                        result.Success = false;
                        if (view.checkdata[0].sameTKNum > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(view.checkdata[0].samelstDetailed[0].status.Trim()))
                            {
                                result.Message = view.checkdata[0].samelstDetailed[0].status.Trim();
                            }
                            else
                            {
                                result.Message = "没返回票信息";
                            }

                        }
                        else if (view.checkdata[0].differentTKNum > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(view.checkdata[0].differentlstDetailed[0].status.Trim()))
                            {
                                result.Message = view.checkdata[0].differentlstDetailed[0].status.Trim();
                            }
                            else
                            {
                                result.Message = "没返回票信息";
                            }

                        }
                        else
                        {
                            result.Message = "客票无效";
                        }
                        result.MsgCode = view.ErrorRes.Err_code;
                    }
                    else if (view.ErrorRes.Err_code == "406")
                    {
                        result.Success = false;
                        result.Message = "";
                        result.MsgCode = view.ErrorRes.Err_code;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "调用接口失败！";

                        result.MsgCode = view.ErrorRes.Err_code;

                    }

                }
                else
                {
                    //result.Result = ParseResponseCore(JsonConvert.DeserializeObject<TResponse>(view.data));
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "ParseResponseCheck");
                result = new ExecResult<TResult>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            return result;
        }
        /// <summary>
        /// 返回信息（验证结果）
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseCenterResponseCheck(string response)
        {
            var result = new ExecResult<TResult>();
            var view = JsonConvert.DeserializeObject<ViewBaseCheck>(response);
            if (view != null)
            {
                result.MsgCode = "0000";
                result.Message = "成功";
                result.Success = true;

            }
            else
            {
                result.MsgCode = "0001";
                result.Message = "失败";
                result.Success = false;
            }

            return result;
        }
        #region 方法
        /// <summary>
        /// 获取请求url
        /// </summary>
        /// <returns></returns>
        private string GetRequestUrl()
        {
            return ServiceAddress + "/" + RequestAddress;
        }
        private string PrepareRequest()
        {
            var parameters = PrepareRequestCore();
            string data = JsonConvert.SerializeObject(parameters);

            return data;
        }
        #endregion
    }
    public abstract class ProcessorBase<T> : ProcessorBase<T, T>
    {
    }
}
