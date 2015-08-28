using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNet.WebApi.Document.Controllers
{
    using System.Web.Http.Description;

    /// <summary>
    /// 测试Controller
    /// </summary>
    public class TestValueController : ApiController
    {
        /// <summary>
        /// Get Method 1 Default
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// Get Method 2
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// Post Method
        /// </summary>
        /// <param name="value">值值</param>
        /// <returns></returns>
        public string Post([FromBody]string value)
        {
            return "Post";
        }
        /// <summary>
        /// Put Method
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="value">Value 值</param>
        /// <returns></returns>
        public string Put(int id, [FromBody]string value)
        {
            return "Put";
        }
        /// <summary>
        /// Delete Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Delete(int id)
        {
            return "Delete";
        }
        /// <summary>
        /// Test1
        /// </summary>
        /// <param name="value">vvv</param>
        /// <returns></returns>
        [ResponseType(typeof(string))]
        public HttpResponseMessage Test1(string value)
        {
            return new HttpResponseMessage { Content = new StringContent(value) };
        }
        /// <summary>
        /// Test2
        /// </summary>
        /// <param name="value">aaaaaa</param>
        /// <returns></returns>
        [ResponseType(typeof(List<string>))]
        public HttpResponseMessage Test2(string value)
        {
            return Request.CreateResponse(new List<string> { "a", "b", "c", value });
        }
    }
}