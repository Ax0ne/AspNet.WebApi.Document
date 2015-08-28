using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.WebApi.Document.ApiProviders
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    using AspNet.WebApi.Document.Models;

    /// <summary>
    /// Api 异常过滤器
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            long elapsed = 0;
            if (actionExecutedContext.Request.Properties.ContainsKey(GlobalConstKey.Action_Duration))
            {
                var stopWatch = actionExecutedContext.Request.Properties[GlobalConstKey.Action_Duration] as Stopwatch;
                stopWatch.Stop();
                elapsed = stopWatch.ElapsedMilliseconds;
            }
            var exception = actionExecutedContext.Exception;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            var customException = exception as ProcedureException;
            var message = exception.Message;
            if (customException != null)
            {
                message = customException.Message;
            }
            else
            {
                IEnumerable<string> headervalue;
                if (actionExecutedContext.Request.Headers.TryGetValues(GlobalConstKey.Api_Exception_Debug_Header, out headervalue))
                {
                    var value = headervalue.First();
                    message += "+++/r/n+++" + exception.StackTrace;
                }
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(new PackageResult<string> { Message = message, Elapsed = elapsed, IsException = true });
        }
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}