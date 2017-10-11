using System;
using Org.BouncyCastle.Crypto.Engines;
using StackExchange.Redis;

namespace CGT.DDD.Caching {
    /// <summary>
    /// 使用redis
    /// </summary>
    public class RedisCache : ICache {

       readonly  static ConfigurationOptions config = new ConfigurationOptions()
        {
            EndPoints = { { "101.200.215.56", 16379 } },
            Password = "myPayRedis_01"
            

        };
        //readonly static string _conn = "localhost:6379";
        readonly static int _expireMinutes = 20;
        readonly static IDatabase cache = ConnectionMultiplexer.Connect(config).GetDatabase();//redis0:6380,redis1:6380,allowAdmin=true

        #region ICache 成员
        /// <summary>
        /// todo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void Put(string key, object obj) {

            cache.StringSet(key,   obj.ToString(), new TimeSpan(0, _expireMinutes, 0));
            
        }

        public void Put(string key, object obj, int expireMinutes) {
            cache.StringSet(key,obj.ToString(), new TimeSpan(0, expireMinutes, 0));
        }

  

        public object Get(string key) {
           
            return cache.StringGet(key);
        }

        public void Delete(string key) {
            throw (new Exception("本删除方法不支持redis,请使用该方法返回bool类型的重载"));
        }

        public bool Delete(string key, object obj) {
            return cache.SetRemove(key, (RedisValue)obj);
        }

        #endregion
    }
}
