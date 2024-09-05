using WxAppWebApi.Comons.Helpers;
using WxAppWebApi.Comons.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WxAppWebApi.Comons.AOP.Filters
{
    /// <summary>
    /// 过滤器，继承了ResourceFilter，用来过滤资源信息，这里用来做缓存判断。
    /// </summary>
    public class MemoryCacheFilter : IResourceFilter
    {
        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="context"></param>
        void IResourceFilter.OnResourceExecuted(ResourceExecutedContext context)
        {
             // MemoryCacheHelper.CacheInsertAddMinutes("name", "wang", 64);
        }

        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        void IResourceFilter.OnResourceExecuting(ResourceExecutingContext context)
        {

            // 获取请求参数
            var queryString = context.HttpContext.Request.QueryString;
            var queryParameters = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.ToString());

            // 通过键名获取特定的参数值
            queryParameters.TryGetValue("vaildcode", out var vaildcode);
            queryParameters.TryGetValue("usermail", out var email);

            var cacheVaildCode = MemoryCacheHelper.GetCacheValue(email);

            // 找不到或者验证不相等，说明验证码错误或者邮箱错误
            if (cacheVaildCode == null|| vaildcode != cacheVaildCode)
                context.Result = new NotFoundObjectResult(ResultTool.Fail("验证码错误或者邮箱错误"));
        }
    }
}
