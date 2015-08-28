using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNet.WebApi.Document.Controllers
{
    using System.Web.Http.Cors;
    using System.Web.Http.Description;

    using AspNet.WebApi.Document.Models;

    /// <summary>
    /// 错误信息
    /// </summary>
    [NotAuthorize, ApiExplorerSettings(IgnoreApi = true), EnableCors("*", "*", "*")]
    public class ErrorController : ApiController
    {
        /// <summary>
        /// 404或请求方法不匹配
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public PackageResult<string> Handle404()
        {
            return new PackageResult<string> { Message = "未找到与控制器匹配的Action，请检查Action名称或请求类型是否被当前Action所允许" };
        }
    }
}
