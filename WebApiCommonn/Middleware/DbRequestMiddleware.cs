using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WebApiCommonn.DataModel;

namespace WebApiCommon.Middleware
{
    public class DbRequestMiddleware
    {
        private RequestDelegate _requestDelegate;
        private ILogger<ExceptionHandlerMiddleware> _logger;
        private IMemoryCache _cache;

        public DbRequestMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger, IMemoryCache cache)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stream originalBody = context.Response.Body;
            string method = context.Request.Method;
            try
            {
                if (method == "GET")
                {
                    await AddToCache(context, originalBody);
                }
                else
                {
                    string path = context.Request.Path;
                    _cache.Remove(path);
                    _cache.Remove(GetFullPath(path));
                    _logger.LogInformation($"remove 1 path");
                    _logger.LogInformation($"remove 2 path");
                    await _requestDelegate.Invoke(context);
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private async Task AddToCache(HttpContext context, Stream originalBody)
        {
            string path = context.Request.Path;

            if (!_cache.TryGetValue(path, out string result))
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    await _requestDelegate.Invoke(context);
                    memStream.Position = 0;
                    result = new StreamReader(memStream).ReadToEnd();
                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                    _logger.LogInformation($"Set result path {path} in cache");
                    _cache.Set(path, result, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                    });
                }

            }
            else
            {
                await context.Response.WriteAsync(result);
                _logger.LogInformation($"Get result path {path} from cache");
            }
        }

        private string GetFullPath(string path)
        {
            var a = path.LastIndexOf("/");
            return path.Substring(0, a - 1);
        }
    }
}
