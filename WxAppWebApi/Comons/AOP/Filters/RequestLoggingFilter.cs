using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Diagnostics;
using System.Text;

namespace WxAppWebApi.Comons.AOP.Filters
{
    /// <summary>
    /// Action过滤器，将请求值和返回值写入到数据库中
    /// </summary>
    public class RequestLoggingFilter : IActionFilter
    {
        private readonly Serilog.ILogger _logger;//注入serilog
        private Stopwatch _stopwatch;//统计程序耗时
        private string EnterTime;
        public RequestLoggingFilter(Serilog.ILogger logger)
        {
            _logger = logger;
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var ip = context.HttpContext.Request.Headers.Host;
            string requestData = "";
            if (context.HttpContext.Request.Method == "POST")
            {
                context.HttpContext.Request.Body.Position = 0;
                using var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8);
                requestData = reader.ReadToEndAsync().Result;
            }
            else
            {
                var request1 = context.HttpContext.Request;
                requestData = System.Web.HttpUtility.UrlDecode(request1.QueryString.ToString(), Encoding.UTF8);
            }

            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            var template = JsonConvert.SerializeObject(context.Result);
            var template1 = (JObject)JsonConvert.DeserializeObject(template);
            var ge = template1["Value"].ToString();
            _logger
                .ForContext("RequestJson", requestData) //请求字符串
                .ForContext("ResponseJson", ge) //响应数据json
                .ForContext("EnterTime", EnterTime)
                .ForContext("IP", ip)
                .Information("Request {Method} {Path} responded {StatusCode} in {Elapsed:0.0000} ms",//message
                request.Method,
                request.Path,
                response.StatusCode,
                _stopwatch.Elapsed.TotalMilliseconds);

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            EnterTime = DateTime.Now.ToString("G");
        }

    }
}
