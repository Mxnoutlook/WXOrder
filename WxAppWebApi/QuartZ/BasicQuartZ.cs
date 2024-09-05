using Quartz;
using Quartz.Impl;

namespace WxAppWebApi.QuartZ
{
    public class BasicQuartZ
    {
        public void Show()
        {
            // 1、创建一个调度器
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            // 2、启动调度器
            scheduler.Start();
            {
                // 3、写几个触发器trigger和job，无所谓先后
                IJobDetail? jobDetail = JobBuilder.Create<MyJob>()
                    // 身份识别,组为了区分不重复
                    .WithIdentity("myjob", "group")
                    .Build();

                TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                    .StartNow()
                    // 执行优先级
                    .WithPriority(10)
                    .ForJob(jobDetail)
                    .WithIdentity("myjob", "group")
                #region    使用原始方式来写
                    //// 执行3次，重复5秒
                    //.WithSimpleSchedule(opt =>
                    //{
                    //    opt.WithIntervalInSeconds(5).WithRepeatCount(3);
                    //})
                    //// 具体到每天什么时候执行
                    //.WithCalendarIntervalSchedule(time => time.WithIntervalInHours(1))
                    //// 用于指定每周的第几天执行，例如每周周六周期，8-20，每两秒执行一次
                    //.WithDailyTimeIntervalSchedule(time =>
                    //{

                    //});
                #endregion
                #region 时间表达式
                    .WithCronSchedule("* * * * * ? *");

                #endregion
                // 4、放到调度器里面进行统一调度
                ITrigger trigger = triggerBuilder.Build();
                scheduler.ScheduleJob(jobDetail, trigger);

            }
        }
    }
}
