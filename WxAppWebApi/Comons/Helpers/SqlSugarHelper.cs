using WxAppWebApi.Comons.Extend;
using Dm;
using Masuit.Tools.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;
using SqlSugar;
using System;

namespace WxAppWebApi.Comons.Helpers
{
    /// <summary>
    /// sqlsugar的辅助类
    /// </summary>
    public class SqlSugarHelper
    {
        public static SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = "Server=localhost;Database=customer_order;Uid=root;Pwd=123456;Allow User Variables=true",
            DbType = DbType.MySql,
            IsAutoCloseConnection = true
        },
           db =>
           {
               db.Aop.OnLogExecuting = (sql, pars) =>
               {
                   var _logManager = new Masuit.Tools.Logging.LogManager();

                   // 将执行的 SQL 语句和参数一起打印出来
                   if (pars != null && pars.Length > 0)
                   {

                       for (int i = 0; i < pars.Length; i++)
                       {
                           var parameter = pars[i];

                           // 获取参数的名称和值
                           string parameterName = parameter.ParameterName;
                           object parameterValue = parameter.Value;

                           // 将参数值替换到 SQL 语句中
                           sql = sql.Replace(parameterName, $"'{parameterValue}'");
                       }
                       // 打印带有参数值的 SQL 语句
                       _logManager.WriteLog(LogExtend.LogExtendLevel.SQL, $"Executed SQL: {sql}");
                   }
                   else
                   {
                       // 如果没有参数，只记录原始 SQL 语句
                       _logManager.WriteLog(LogExtend.LogExtendLevel.SQL, $"Executing SQL: {sql}");
                   }
               };
           }

      );

    }
}
