using System.ComponentModel;

namespace WxAppWebApi.Comons.Result
{
    /// <summary>
    /// 返回结果状态码，100失败，200成功
    /// </summary>
    public class Resultcode
    {
        public enum ResultCode
        {

            [Description("操作失败！")]
            Fail = 100,
            [Description("请求成功！")]
            Success = 200,
            [Description("未找到!")]
            NotFind =404,

        }
    }
}
