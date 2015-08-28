using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace AspNet.WebApi.Document.ApiProviders
{
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using AspNet.WebApi.Document.Models;

    public class ApiAuthorizationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var isNotAuthorize = actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<NotAuthorizeAttribute>().Any();
            if (isNotAuthorize)
            {
                return;
            }
            isNotAuthorize = actionContext.ActionDescriptor.GetCustomAttributes<NotAuthorizeAttribute>().Any();
            if (isNotAuthorize)
            {
                return;
            }
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues(GlobalConstKey.Api_Authorization_Header, out values))
            {
                // 做其他处理
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(new PackageResult<string> { Message = "未经认证的请求来源" });
            }
        }
    }
}

namespace AspNet.WebApi.Document
{
    /// <summary>
    /// 不需要授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    internal sealed class NotAuthorizeAttribute : Attribute
    {

    }
}