using Microsoft.AspNetCore.Mvc.Filters;

namespace WxAppWebApi.Comons.Attributes
{
    /// <summary>
    /// 一个特性，继承了CustomerBaseAopAttribute
    /// </summary>
    public class CustomerCacheAttribute : CustomerBaseAopAttribute
    {

        #region 抽象类的方式


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("执行后");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("执行前");
        }

        #endregion 

        #region  异步和非异步方式拦截器
        ///// <summary>
        ///// 执行后
        ///// </summary>
        ///// <param name="context"></param>
        ///// <exception cref="NotImplementedException"></exception>
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    Console.WriteLine("执行后");
        //}

        ///// <summary>
        ///// 执行前
        ///// </summary>
        ///// <param name="context"></param>
        ///// <exception cref="NotImplementedException"></exception>
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    Console.WriteLine("执行前");
        //}

        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    Console.WriteLine("缓存前");
        //    await next();
        //    Console.WriteLine("hua执行后");
        //}
        #endregion
    }
}
