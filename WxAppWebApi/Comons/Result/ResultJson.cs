using static WxAppWebApi.Comons.Result.Resultcode;

namespace WxAppWebApi.Comons.Result
{
    public class ResultJson
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string? Msg { get; set; }
        public object? Data { get; set; }
        public ResultJson() { }
        public ResultJson(bool success)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)ResultCode.Fail;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
        }

        public ResultJson(bool success, string msg)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)ResultCode.Fail;
            Msg = msg;
        }

        public ResultJson(bool success, ResultCode resultEnum)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)resultEnum;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
        }

        public ResultJson(bool success, object data)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)ResultCode.Fail;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
            Data = data;
        }
        public ResultJson(bool success, ResultCode resultEnum, object data)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)resultEnum;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
            Data = data;
        }
        public ResultJson(bool success, ResultCode resultEnum, string msg)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)resultEnum;
            Msg = msg;
        }

    }
}
