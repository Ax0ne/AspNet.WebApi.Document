using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumCms.WebApi
{
    using System.Web.Http.Controllers;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.ValueProviders;


    public class CustomModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            //if (bindingContext.ModelType == typeof(ParaBase))
            //{
            //    var paramsValue = (KeyValuePair<string, string>[])actionContext.Request.Properties["MS_QueryNameValuePairs"];
            //    if (paramsValue.Length > 0)
            //    {
            //        var userName = paramsValue.First(s => s.Key.ToLower().Contains("username")).Value;
            //        bindingContext.Model = new ParaBase { SzUserName = userName };
            //    }
            //}
            return true;
        }
    }
}