using Microsoft.AspNetCore.Http;
using System;

namespace CGT.DDD.Utils.Http {
    /// <summary>
    /// 获取api请求上线文信息
    /// </summary>
    public static class CGTHttpContext {
        /// <summary>
        /// 。net容器
        /// </summary>
        public static IServiceProvider ServiceProvider;

        public static IHttpContextAccessor _accessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;
        static CGTHttpContext() { }

        /// <summary>
        /// 实现获取容器中的上下文信息
        /// </summary>
        //public static HttpContext Current {
        //    get {
        //        return GetHttpContext();
        //    }
        //}

        public static HttpContext GetHttpContext() {
            object factory = ServiceProvider.GetService(typeof(HttpContext));
            //HttpContext context = _accessor.HttpContext;
            return _accessor.HttpContext;
            //object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor));
            //HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
            //return context;
        }
    }
}
