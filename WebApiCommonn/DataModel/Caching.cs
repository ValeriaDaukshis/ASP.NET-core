using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using WebApiCommon.Interfaces;

namespace WebApiCommon.DataModel
{
    public class Caching<TReturn> : ICaching<TReturn>
    {
        private readonly IMemoryCache _cache;
        public Caching(IMemoryCache cache)
        {
            _cache = cache;
        }
        public TReturn SetInCache(string cacheKey, Func<int, TReturn> action, int id)
        {
            TReturn obj = action.Invoke(id);
            _cache.Set(cacheKey, obj, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            });

            return obj;
        }

        public IEnumerable<TReturn> SetInCache(string cacheKey, Func<IEnumerable<TReturn>> action)
        {
            IEnumerable<TReturn> obj = action.Invoke();
            _cache.Set(cacheKey, obj, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            });
            return obj;
        }

        public bool CheckInCache(string cacheKey)
        {
            if (_cache.TryGetValue(cacheKey, out _))
                return true;
            return false;
        }

        public TReturn ReturnSingleValueByKey(string cacheKey)
        {
            if (!_cache.TryGetValue(cacheKey, out TReturn value))
                throw new Exception("Ups. Can't get value from cache");
            return value;
        }

        public IEnumerable<TReturn> ReturnValueByKey(string cacheKey)
        {
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<TReturn> value))
                throw new Exception("Ups. Can't get value from cache");
            return value;
        }

        public void RemoveValueFromCache(string cacheKey)
        {
            if(_cache.TryGetValue(cacheKey, out _))
            {
                _cache.Remove(cacheKey);
            }
        }
    }
}
