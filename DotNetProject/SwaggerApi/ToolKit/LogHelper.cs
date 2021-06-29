using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace SwaggerApi
{
    public static class LogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 输出 ERROR 日志
        /// </summary>
        /// <param name="message">文本消息</param>
        public static void Error(string message,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Error, message, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 ERROR 日志
        /// </summary>
        /// <param name="message">异常消息</param>
        public static void Error(Exception throwable,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Error, throwable, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 WARN 日志
        /// </summary>
        /// <param name="message">文本消息</param>
        public static void Warn(string message,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Warn, message, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 WARN 日志
        /// </summary>
        /// <param name="message">异常消息</param>
        public static void Warn(Exception throwable,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Warn, throwable, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 INFO 日志
        /// </summary>
        /// <param name="message">文本消息</param>
        public static void Info(string message,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Info, message, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 INFO 日志
        /// </summary>
        /// <param name="message">异常消息</param>
        public static void Info(Exception throwable,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Info, throwable, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 DEBUG 日志
        /// </summary>
        /// <param name="message">文本消息</param>
        public static void Debug(string message,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Debug, message, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 DEBUG 日志
        /// </summary>
        /// <param name="message">异常消息</param>
        public static void Debug(Exception throwable,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Debug, throwable, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 TRACE 日志
        /// </summary>
        /// <param name="message">文本消息</param>
        public static void Trace(string message,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Trace, message, memberName, sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// 输出 TRACE 日志
        /// </summary>
        /// <param name="message">异常消息</param>
        public static void Trace(Exception throwable,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(LogLevel.Trace, throwable, memberName, sourceFilePath, sourceLineNumber);
        }

        // --------------------------------------------------------------------------------------------

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">文本消息</param>
        /// <param name="memberName">方法名</param>
        /// <param name="sourceFilePath">文件</param>
        /// <param name="sourceLineNumber">行号</param>
        private static void Log(LogLevel level, string message, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (logger.IsEnabled(level))
            {
                var caller = GetCaller(memberName, sourceFilePath, sourceLineNumber);
                logger.Log(level, "[{0}] _ [{1}]", caller, message);
            }
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="throwable">异常消息</param>
        /// <param name="memberName">方法名</param>
        /// <param name="sourceFilePath">文件</param>
        /// <param name="sourceLineNumber">行号</param>
        private static void Log(LogLevel level, Exception throwable, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (logger.IsEnabled(level))
            {
                var caller = GetCaller(memberName, sourceFilePath, sourceLineNumber);
                var message = GetExceptionInfo(throwable);
                logger.Log(level, "[{0}] _ [{1}]", caller, message);
            }
        }

        /// <summary>
        /// 获取调用者信息
        /// </summary>
        /// <param name="memberName">方法名</param>
        /// <param name="sourceFilePath">文件</param>
        /// <param name="sourceLineNumber">行号</param>
        /// <returns></returns>
        private static string GetCaller(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            var source = String.IsNullOrEmpty(sourceFilePath) ? String.Empty : sourceFilePath.Substring(sourceFilePath.LastIndexOf('\\') + 1);

            return $"{memberName ?? String.Empty}({source}:{sourceLineNumber})";
        }

        /// <summary>
        /// 获取详细的异常描述
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        private static string GetExceptionInfo(Exception ex)
        {
            if (ex == null) return String.Empty;

            var message = new StringBuilder(ex.Message);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                message.AppendFormat(" => {0}", ex.ToString());
            }

            return message.ToString();
        }

    }
}
