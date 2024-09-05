
using Autofac;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WxAppWebApi.Comons.AOP.Middleware;
using WxAppWebApi.Comons.Extend;
using WxAppWebApi.QuartZ;
using WxAppWebApi.service.EmailService;
using WxAppWebApi.service.EmailService.Impl;
using WxAppWebApi.service.FileService;
using WxAppWebApi.service.FileService.Impl;
using WxAppWebApi.service.OrderService;
using WxAppWebApi.service.OrderService.Impl;
using WxAppWebApi.service.UserService;
using WxAppWebApi.service.UserService.Impl;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


#region 请求过滤器，获得请求和返回值的过滤器
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(typeof(RequestLoggingFilter));
//});
#endregion



# region 定时任务
new BasicQuartZ().Show();
#endregion

// 为获得IP地址
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

# region 特性注解拦截器
builder.Services.AddMvc(option =>
{
  //  option.Filters.Add<CustomerCacheAttribute>();
    //  option.Filters.Add<MemoryCacheFilter>();
});
#endregion




# region 添加serilog
builder.Host.AddSerilLog();
#endregion

#region autofac的aop
////植入到内置的ServiceProviderFactory
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//{
//    //EnableInterfaceInterceptors启用接口拦截器。
//    containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerDependency().EnableInterfaceInterceptors();
//    // 这就是拦截的类。
//    containerBuilder.RegisterType<AutoFacInterceptDemo>();
//});
#endregion

#region 依赖注入server模块和拦截器
builder.Services.AddSingleton<IFileService, FileServiceImpl>();
builder.Services.AddSingleton<IEmailService, EmailServiceImpl>();
builder.Services.AddSingleton<IUserService, UserServiceImpl>();
builder.Services.AddSingleton<IOrderService, OrderServerImpl>();
#endregion
var app = builder.Build();
#region  获得Post中的参数
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next(context);
});
#endregion

#region 启用静态资源访问
app.UseStaticFiles();
#endregion


# region  异常捕获的中间件
app.UseMiddleware<ExceptionHandlerMiddleware>();
#endregion
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
