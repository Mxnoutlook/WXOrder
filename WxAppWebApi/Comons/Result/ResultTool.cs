using WxAppWebApi.Comons.Helpers;
using static WxAppWebApi.Comons.Result.Resultcode;

namespace WxAppWebApi.Comons.Result
{
    /// <summary>
    /// 返回值工具，用来传递是成功还是失败的实体类的工具。
    /// </summary>
    public static class ResultTool
    {
        /// <summary>
        /// 通过字典存储返回值对应的含义
        /// </summary>
        public static Dictionary<ResultCode, string> DescriptionsDictionary = EnumHelper.GetDescriptions<ResultCode>();

        public static ResultJson Success()
        {
            return new ResultJson(true);
        }

        public static ResultJson Success(object data)
        {
            return new ResultJson(true, data);
        }

        public static ResultJson Fail(double v)
        {
            return new ResultJson(false);
        }

        public static ResultJson Fail(ResultCode resultCode)
        {
            return new ResultJson(false, resultCode);
        }

        public static ResultJson Fail(ResultCode resultCode, string msg)
        {
            return new ResultJson(false, resultCode, msg);
        }

        public static ResultJson Fail(string msg)
        {
            return new ResultJson(false, msg);
        }

        public static ResultJson Fail(bool b, ResultCode resultCode, string message)
        {
            return new ResultJson(false, resultCode, message);
        }
    }
}
