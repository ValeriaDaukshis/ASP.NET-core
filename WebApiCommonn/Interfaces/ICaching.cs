using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiCommon.Interfaces
{
    public interface ICaching<TReturn>
    {
        public TReturn SetInCache(string cacheKey, Func<int, TReturn> action, int id);
        public IEnumerable<TReturn> SetInCache(string cacheKey, Func<IEnumerable<TReturn>> action);
        public bool CheckInCache(string cacheKey);
        public TReturn ReturnSingleValueByKey(string cacheKey);
        public IEnumerable<TReturn> ReturnValueByKey(string cacheKey);
        public void RemoveValueFromCache(string cacheKey);
    }
}
