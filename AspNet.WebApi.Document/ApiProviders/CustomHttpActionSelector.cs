using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using AspNet.WebApi.Document.Controllers;

namespace AspNet.WebApi.Document.ApiProviders
{
    using AspNet.WebApi.Document.Models;

    /// <summary>
    /// 自定义Action选择器
    /// </summary>
    public class CustomHttpActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                    throw;
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Handle404";
                IHttpController httpController = new ErrorController();
                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());
                decriptor = base.SelectAction(controllerContext);
            }
            return new CustomHttpActionDescriptor((ReflectedHttpActionDescriptor)decriptor);
        }
    }

    public class CustomHttpActionDescriptor : ReflectedHttpActionDescriptor
    {
        public CustomHttpActionDescriptor(ReflectedHttpActionDescriptor actionDescriptor)
            : base(actionDescriptor.ControllerDescriptor, actionDescriptor.MethodInfo)
        {
        }

        public override IActionResultConverter ResultConverter
        {
            get
            {
                Type instanceType;
                if (ReturnType.IsGenericType && ReturnType.Name.Equals("PackageResult`1"))
                {
                    var genericType = ReturnType.GenericTypeArguments[0];
                    instanceType = typeof(ApiResultPackageConverter<>).MakeGenericType(new[] { genericType });
                }
                else
                {
                    instanceType = typeof(ApiResultPackageConverter<>).MakeGenericType(new[] { ReturnType });
                }
                var body = Expression.New(instanceType);
                return Expression.Lambda<Func<IActionResultConverter>>(body, new ParameterExpression[0]).Compile().Invoke();
            }
        }
    }
    /// <summary>
    /// Api 结果包装转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResultPackageConverter<T> : IActionResultConverter
    {
        public HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult)
        {
            var httpResponseMessage = actionResult as HttpResponseMessage;
            if (httpResponseMessage != null)
            {
                httpResponseMessage.RequestMessage = controllerContext.Request;
                return httpResponseMessage;
            }
            //var httpActionResult = actionResult as IHttpActionResult;
            //if (httpActionResult != null)
            //{
            //    return actionResult;
            //}
            //var result = new PackageResult<T>(){};
            long elapsed = 0;
            if (controllerContext.Request.Properties.ContainsKey(GlobalConstKey.Action_Duration))
            {
                var stopWatch = controllerContext.Request.Properties[GlobalConstKey.Action_Duration] as Stopwatch;
                stopWatch.Stop();
                elapsed = stopWatch.ElapsedMilliseconds;
            }
            var result = actionResult as PackageResult<T>;
            if (result != null)
            {
                result.Elapsed = elapsed;
            }
            else
            {
                result = new PackageResult<T> { Data = (T)actionResult, Elapsed = elapsed };
            }
            if (string.IsNullOrEmpty(result.Message))
            {
                result.Message = "Api一路通顺（^_^）";
            }
            return controllerContext.Request.CreateResponse(result);
        }
    }
}