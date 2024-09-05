using System.Net;
using WxAppWebApi.Comons.Except;
using WxAppWebApi.Comons.Extend;
using WxAppWebApi.Comons.Result;
using static WxAppWebApi.Comons.Result.Resultcode;

namespace WxAppWebApi.Comons.AOP.Middleware
{
    /// <summary>
    /// 异常捕获，如果失败记录日志并返回实体类
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            ResultJson result;
            // 使用 _logManager 记录日志
            var _logManager = new Masuit.Tools.Logging.LogManager();
            _logManager.WriteLog(LogExtend.LogExtendLevel.Error, exception.Message, exception);
            if (exception is BusinessException businessException)
            {
                // BusinessException处理
                result = ResultTool.Fail(businessException.ErrorCode, businessException.Message);
            }
            else
            {
                // 其他未处理异常处理
                result = ResultTool.Fail(ResultCode.Fail, exception.Message);
            }
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
