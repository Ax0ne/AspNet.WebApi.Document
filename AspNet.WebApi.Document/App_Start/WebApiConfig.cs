using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace AspNet.WebApi.Document
{
    using System.Web.Http.ModelBinding;
    using System.Web.Http.ModelBinding.Binders;

    using AspNet.WebApi.Document.ApiProviders;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.MessageHandlers.Add()
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Error404",
                routeTemplate: "{*url}",
                defaults: new { controller = "Error", action = "Handle404" }
            );
            config.Filters.Add(new ApiActionExcutionTimerFilter());
            config.Filters.Add(new ApiExceptionFilter());
            config.Filters.Add(new ApiAuthorizationFilter());
            //var provider = new SimpleModelBinderProvider(typeof(ParaBase), new CustomModelBinder());
            //config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
