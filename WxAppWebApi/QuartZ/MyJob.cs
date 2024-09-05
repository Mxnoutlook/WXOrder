using WxAppWebApi.Comons.Extend;
using Masuit.Tools.Logging;
using Quartz;
using System;

namespace WxAppWebApi.QuartZ
{
    public class MyJob : IJob
    {
        // 任务的具体内容
        public MyJob()
        {
          //  var _logManager = new LogManager();

        //    _logManager.WriteLog(LogExtend.LogExtendLevel.Info, DateTime.Now.ToString("G"));
        }
        // 调用该接口就需要调用该方法
        public async Task Execute(IJobExecutionContext context)
        {

           // await Task.CompletedTask;
        }
    }
}
