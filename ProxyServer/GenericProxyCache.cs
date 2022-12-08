using ProxyServer.JCDECAUXRestClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer
{
    public class GenericProxyCache<T> where T : class
    {
        private ObjectCache cache = MemoryCache.Default;
        private DateTimeOffset DefaultCacheTimeOffset;
        
        public GenericProxyCache()
        {
            DefaultCacheTimeOffset = new DateTimeOffset();
            
        }

        public GenericProxyCache(int hours, int minutes, int seconds) : this()
        {
            long dt_seconds = hours * 3600 + minutes * 60 + seconds;
            DefaultCacheTimeOffset = DateTimeOffset.Now.AddSeconds(dt_seconds);
        }
        
        public T Get(string key, DateTimeOffset dto)
        {
            if(cache.Get(key) == null)
            {
                object obj = (T)Activator.CreateInstance(typeof(T), new object[] { key });
                cache.Add(key, obj, dto);
            }
            return (T)cache.Get(key);
        }

        public T Get(string key, double dto_s)
        {
            return Get(key, DateTimeOffset.Now.AddSeconds(dto_s));
        }

        public T Get(string key)
        {
            return Get(key, DefaultCacheTimeOffset);
        }

    }
}
