using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.WebApi.Document.Models
{
    /// <summary>
    /// 结果包装对象
    /// </summary>
    public class PackageResult<T>
    {
        /// <summary>
        /// 返回的真实结果数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Api内部处理耗时（毫秒）
        /// </summary>
        public long Elapsed { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        public bool IsException { get; set; }
        /// <summary>
        /// 结果响应附加消息（包括异常消息）
        /// </summary>
        public string Message { get; set; }
        ///// <summary>
        ///// 处理状态
        ///// </summary>
        //public ResultState State { get; set; }
    }
    /// <summary>
    /// 结果状态
    /// </summary>
    public enum ResultState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParameterError = 3,
        /// <summary>
        /// 授权错误
        /// </summary>
        AuthorizeError = 4
    }
}