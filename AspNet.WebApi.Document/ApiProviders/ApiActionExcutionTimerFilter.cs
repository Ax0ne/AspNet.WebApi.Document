using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Diagnostics;

namespace AspNet.WebApi.Document.ApiProviders
{
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using AspNet.WebApi.Document.Models;

    /// <summary>
    /// ApiAction执行计时过滤器
    /// </summary>
    public class ApiActionExcutionTimerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false || (actionContext.ActionArguments.Count == 1 && actionContext.ActionArguments.First().Value == null))
            {
                var returnType = actionContext.ActionDescriptor.ReturnType;
                //var instanceType = typeof(PackageResult<>).MakeGenericType(new[] { returnType });
                //var instance = Activator.CreateInstance(instanceType);
                if (actionContext.ActionArguments.Count == 1 && actionContext.ActionArguments.First().Value == null)
                {
                    var argument = actionContext.ActionArguments.First();
                    actionContext.Response = actionContext.Request.CreateResponse(new PackageResult<string> { Message = string.Format("参数验证错误：{0} 不能为空", argument.Key) });
                }
                else
                {
                    var errorMessages = actionContext.ModelState.Select(m => m.Value).Select(s => s.Errors).Select(s => s[0]).Select((s, i) => string.Format("{0}. " + s.ErrorMessage, i + 1)).ToList();
                    actionContext.Response = actionContext.Request.CreateResponse(new PackageResult<string> { Message = string.Format("参数验证错误：{0}", String.Join(" ", errorMessages)) });
                }
            }
            else
            {
                if (actionContext.ActionArguments.Count > 0) // 公共参数验证
                {
                    //if (actionContext.ActionArguments.Count == 1)
                    //{
                    //    //var enumerableValue = actionContext.ActionArguments.First().Value as IEnumerable<int>;
                    //    //if (enumerableValue != null && !enumerableValue.Any())
                    //    //{
                    //    //    actionContext.Response = actionContext.Request.CreateResponse(new PackageResult<string> { Message = "参数不包含任何值" });
                    //    //    return;
                    //    //}
                    //    var paraValue = actionContext.ActionArguments.First().Value as ParaBase;
                    //    if (paraValue != null)
                    //    {
                    //        var paraBase = paraValue;
                    //        var userId = IocContainer.Resolve<IWebUserRepository>().ConvertUserId(paraBase.SzUserName);
                    //        if (userId == 0)
                    //        {
                    //            actionContext.Response = actionContext.Request.CreateResponse(new PackageResult<string> { Message = "用户不存在" });
                    //            return;
                    //        }
                    //        paraBase.UserId = userId;
                    //    }
                    //}
                    //else
                    //{
                    //    var keyValues = actionContext.ActionArguments.Select(s => new KeyValuePair<string, object>(s.Key.ToLower(), s.Value));
                    //    var paraObject = keyValues.FirstOrDefault(p => p.Key.Contains("username"));
                    //    var userName = paraObject.Value as string;
                    //    var userId = IocContainer.Resolve<IWebUserRepository>().ConvertUserId(userName);
                    //    if (userId == 0)
                    //    {
                    //        actionContext.Response = actionContext.Request.CreateResponse(new PackageResult<string> { Message = "用户不存在" });
                    //        return;
                    //    }
                    //}
                }
                // 记录Api执行时间,默认对所有Action启用
                var stopWatch = new Stopwatch();
                actionContext.ControllerContext.Request.Properties[GlobalConstKey.Action_Duration] = stopWatch;
                stopWatch.Start();
            }
        }
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //if (!actionExecutedContext.Request.Properties.ContainsKey(_key))
            //{
            //    return;
            //}
            //var stopWatch = actionExecutedContext.Request.Properties[_key] as Stopwatch;
            //if (stopWatch != null)
            //{
            //    actionExecutedContext.Request.Properties["__action_duration__milliseconds"] = stopWatch.ElapsedMilliseconds;
            //}
        }
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }

}