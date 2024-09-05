using Castle.DynamicProxy;
using WxAppWebApi.Comons.AttributeThree;

namespace WxAppWebApi.Comons.AOP.DynamicProxy
{

    /// <summary>
    /// 使用AutoFac AOP的具体业务——实现IInterceptor,拦截server层的东西。
    /// </summary>
    public class AutoFacInterceptDemo : IInterceptor
    {
        void IInterceptor.Intercept(IInvocation invocation)
        {
            if (invocation.Method.IsDefined(typeof(IngoreAutointerAttribute), true))
            {
                invocation.Proceed();
                return;
            }

            Console.WriteLine(invocation.Method.Name);
            Console.WriteLine("方法前面做……");
            // 这就是执行的具体方法
            invocation.Proceed();
            Console.WriteLine("方法后面做……");

        }
    }
}
