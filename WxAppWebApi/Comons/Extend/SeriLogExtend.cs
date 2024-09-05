using Serilog.Events;
using Serilog;
using Serilog.Sinks.MariaDB.Extensions;
using Serilog.Sinks.MariaDB;
using System.Runtime.InteropServices;
using System.Text.Unicode;

namespace WxAppWebApi.Comons.Extend
{
    /// <summary>
    /// serilog的日志信息配置类
    /// </summary>
    public static class SeriLogExtend
    {
        public static void AddSerilLog(this ConfigureHostBuilder configureHostBuilder)
        {
            //输出模板
            string outputTemplate = "{NewLine}【{Level:u3}】{Timestamp:yyyy-MM-dd HH:mm:ss.fff}" +
                                    "{NewLine}#Msg#{Message:lj}" +
                                    "{NewLine}#Pro #{Properties:j}" +
                                    "{NewLine}#Exc#{Exception}" +
                                     new string('-', 50);

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", true, true);
            var ConfigRoot = configurationBuilder.Build();
            string con_str = ConfigRoot.GetSection("ConnectionStrings")["DbConnectionString"];


            // 配置Serilog
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // 排除Microsoft的日志
            .Enrich.FromLogContext() // 注册日志上下文
            .WriteTo.MariaDB(connectionString: @"server=localhost; Database = customer_order; AllowLoadLocalInfile = true; User ID = root; Password = 123456; allowPublicKeyRetrieval = true; pooling = true; CharSet = utf8; port = 3306", tableName: "Logss", autoCreateTable: true, useBulkInsert: false, options: new MariaDBSinkOptions
            {
                PropertiesToColumnsMapping =
                {
                    ["RequestJson"]="RequestJson",
                    ["ResponseJson"]="ResponseJson",
                    ["EnterTime"]="EnterTime",
                    ["Exception"] = null,
                    ["Properties"]=null,
                    ["MessageTemplate"]=null,
                    ["Timestamp"]=null,
                    ["IP"]="IP"
                },
            })
            .WriteTo.Logger(configure => configure // 输出到文件
                  .MinimumLevel.Debug()
                  .WriteTo.File(  //单个日志文件，总日志，所有日志存到这里面
                      $"logs\\log.txt",
                      rollingInterval: RollingInterval.Day,
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                  //.WriteTo.RollingFile( //每天生成一个新的日志，按天来存日志
                  //   "logs\\{Date}-log.txt", //定输出到滚动日志文件中，每天会创建一个新的日志，按天来存日志
                  //   retainedFileCountLimit: 7,
                  //   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                  )
         .CreateLogger();
            configureHostBuilder.UseSerilog(Log.Logger); // 注册serilog

        }
    }
}
