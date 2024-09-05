using Masuit.Tools.Logging;

namespace WxAppWebApi.Comons.Extend
{
    /// <summary>
    /// 扩展masuit.tool 中的日志方法，使用WriteLog方法， LogExtendLevel 标识日志信息等级。
    /// </summary>
    public static class LogExtend
    {
        public static void WriteLog(this LogManager log, LogExtendLevel type, string? mes = null, Exception? ex = null)
        {

            LogManager.LogDirectory = AppDomain.CurrentDomain.BaseDirectory + "/MaSuit_logs/" + type;
            switch (type)
            {
                case LogExtendLevel.Info:
                    LogManager.Info(mes);
                    break;
                case LogExtendLevel.Error:
                    LogManager.Error(ex);
                    break;
                case LogExtendLevel.Fatal:
                    LogManager.Fatal(ex);
                    break;
                case LogExtendLevel.SQL:
                    LogManager.Info(mes);
                    break;
            }
        }
        public enum LogExtendLevel
        {
            Info,
            Error,
            Fatal,
            SQL
        }
    }


}
