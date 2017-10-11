using System;
using Microsoft.Extensions.Caching.Memory;

namespace CGT.DDD.Caching
{
    /// <summary>
    /// 运行时缓存，基于服务端内存存储
    /// </summary>
    public class RuntimeCache : ICache
    {
        private IMemoryCache httpRuntimeCache;

        public RuntimeCache(IMemoryCache _httpRuntimeCache)
        {
            httpRuntimeCache = _httpRuntimeCache;
        }

        readonly static int _expireMinutes = 20;//缓存配置时间

        #region ICache 成员

        public void Put(string key, object obj)
        {
            httpRuntimeCache.Set(key, obj);
        }

        public void Put<T>(string key, object obj)
        {
            httpRuntimeCache.Set<T>(key, (T)obj);
        }

        public void Put(string key, object obj, int expireMinutes)
        {
            httpRuntimeCache.Set(key, obj, new TimeSpan(0, expireMinutes, 0));
        }

        public object Get(string key)
        {
            return httpRuntimeCache.Get(key);
        }

        public void Delete(string key)
        {
            httpRuntimeCache.Remove(key);
        }

        #endregion
    }
}
