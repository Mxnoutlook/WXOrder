using static WxAppWebApi.Comons.Result.Resultcode;

namespace WxAppWebApi.Comons.Result
{
    public class JsonResult
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string? Msg { get; set; }
        public object? Data { get; set; }
        public JsonResult() { }
        public JsonResult(bool success)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)ResultCode.Fail;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
        }

        public JsonResult(bool success, string msg)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)ResultCode.Fail;
            Msg = msg;
        }

        public JsonResult(bool success, ResultCode resultEnum)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)resultEnum;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
        }

        public JsonResult(bool success, object data)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)ResultCode.Fail;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
            Data = data;
        }
        public JsonResult(bool success, ResultCode resultEnum, object data)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)resultEnum;
            Msg = ResultTool.DescriptionsDictionary[(ResultCode)Code];
            Data = data;
        }
        public JsonResult(bool success, ResultCode resultEnum, string msg)
        {
            Success = success;
            Code = success ? (int)ResultCode.Success : (int)resultEnum;
            Msg = msg;
        }

    }
}
