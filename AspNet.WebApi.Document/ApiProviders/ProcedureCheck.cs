using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.WebApi
{
    /// <summary>
    /// 流程检查
    /// </summary>
    public static class ProcedureCheck
    {
        /// <summary>
        /// 用于阻止方法内部继续执行
        /// </summary>
        /// <param name="message">传给<see cref="T:AspNet.WebApi.Document.Models.PackageResult.Message"/>的消息</param>
        public static void Throw(string message)
        {
            throw new ProcedureException(message);
        }
    }

    /// <summary>
    /// 流程异常
    /// </summary>
    public class ProcedureException : Exception
    {
        /// <summary>
        /// 用于阻止方法内部继续执行
        /// </summary>
        /// <param name="message">传给<see cref="T:AspNet.WebApi.Document.Models.PackageResult.Message"/>的消息</param>
        public ProcedureException(string message)
            : base("【流程异常】" + message)
        {

        }
    }
}