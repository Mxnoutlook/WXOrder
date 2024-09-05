using static WxAppWebApi.Comons.Result.Resultcode;

namespace WxAppWebApi.Comons.Except
{
    /// <summary>
    /// 继承Exception ，获得错误信息的返回码。
    /// </summary>
    public class BusinessException : Exception
    {
        public ResultCode ErrorCode { get; set; }
        public BusinessException(string message) : base(message)
        {
            ErrorCode = ResultCode.Fail;
        }
        public BusinessException(ResultCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
