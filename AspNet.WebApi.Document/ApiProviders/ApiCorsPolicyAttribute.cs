using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Cors;
using System.Web;

namespace AspNet.WebApi.Document
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Cors;

    /// <summary>
    /// 表示允许跨域请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ApiCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private readonly CorsPolicy _policy;
        /// <summary>
        /// 表示允许跨域请求
        /// </summary>
        public ApiCorsPolicyAttribute()
        {
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };
            _policy.Origins.Add("http://2.16898.cc");
            _policy.Origins.Add("http://198.18.0.254:8009/");
            _policy.Origins.Add("http://localhost:5405");
        }
        /// <summary>
        /// 表示允许跨域请求
        /// </summary>
        /// <param name="origins">允许的Cors来源Url，默认对所有来源启用</param>
        public ApiCorsPolicyAttribute(string origins = null)
        {
            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };
            if (string.IsNullOrEmpty(origins))
            {
                _policy.AllowAnyOrigin = true;
            }
            else
            {
                // Add allowed origins.
                _policy.Origins.Add("http://2.16898.cc");
                _policy.Origins.Add("http://198.18.0.254:8009/");
                _policy.Origins.Add("http://localhost:4560");
                origins.Split(',').ToList().ForEach(s => _policy.Origins.Add(s));
            }
        }
        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}