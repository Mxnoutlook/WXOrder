using WxAppWebApi.EmailUtils;
using WxAppWebApi.Entity;

namespace WxAppWebApi.service.EmailService
{
    /// <summary>
    /// 邮件处理的接口方法
    /// </summary>
    public interface IEmailService
    {

        Task<SendResultEntity>  SendMail();

        /// <summary>
        /// 1、发送验证码的邮件
        /// 2、无附件
        /// 3、邮件格式——发送验证码的格式
        /// </summary>
        /// <param name="VaildCode">验证码内容</param>
        /// <param name="Addressee">收件人地址</param>
        /// <returns></returns>
        Task<SendResultEntity> SendVaildEmail(string VaildCode,string Addressee);



        Task<SendResultEntity> SendOrderEmail(TbOrder tbOrder);
    }
}
