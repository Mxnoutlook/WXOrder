using Microsoft.AspNetCore.Mvc.Filters;

namespace WxAppWebApi.Comons.Attributes
{
    /// <summary>
    /// Action_Filter的Base过滤器，提供给Action_Filter继承
    /// </summary>
    public abstract class CustomerBaseAopAttribute :ActionFilterAttribute
    {

        //ActionFilterAttribute 是简化版的特性和过滤器

        public abstract override void OnActionExecuted(ActionExecutedContext context);
        public abstract override void OnActionExecuting(ActionExecutingContext context);
    }
}
