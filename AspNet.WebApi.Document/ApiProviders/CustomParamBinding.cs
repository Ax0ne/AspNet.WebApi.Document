using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumCms.WebApi
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Metadata;

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public class CustomParamBindingAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor paramDesc)
        {
            return new CustomParamBinding(paramDesc);
        }
    }

    public class CustomParamBinding : HttpParameterBinding
    {
        public CustomParamBinding(HttpParameterDescriptor paramDesc) : base(paramDesc) { }

        public override bool WillReadBody
        {
            get
            {
                return false;
            }
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            string idsAsString = actionContext.Request.GetRouteData().Values["ids"].ToString();
            idsAsString = idsAsString.Trim('[', ']');

            IEnumerable<string> ids = idsAsString.Split(',');
            ids = ids.Where(str => !string.IsNullOrEmpty(str));

            IEnumerable<int> idList = ids.Select(strId =>
            {
                if (string.IsNullOrEmpty(strId)) return -1;

                return Convert.ToInt32(strId);

            }).ToArray();

            SetValue(actionContext, idList);

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }
    }
}