using WxAppWebApi.Comons.AOP.Filters;
using WxAppWebApi.Comons.Extend;
using WxAppWebApi.Comons.Helpers;
using WxAppWebApi.Comons.Result;
using WxAppWebApi.EmailUtils;
using WxAppWebApi.Entity;
using WxAppWebApi.service.EmailService;
using Masuit.Tools;
using Masuit.Tools.Models;
using Masuit.Tools.Strings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;
using WxAppWebApi.service.OrderService;

namespace WxAppWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IOrderService _orderService;

        public MailController(IEmailService emailService, IOrderService orderService)
        {
            _emailService = emailService;
            _orderService = orderService;
        }

        [HttpGet(Name = "SendVaildEmail")]
        public async Task<ResultJson> SendVaildEmail(String Addressee)
        {
            // 1、验证是否是邮箱
            if (!Addressee.IsEmail())
                return ResultTool.Fail($"{Addressee}不是合格的邮箱地址");

            // 2、随机生成验证码
            var code = ValidateCode.CreateValidateCode(6);
            var temo = ResultTool.Success(await _emailService.SendVaildEmail(code, Addressee));
            MemoryCacheHelper.CacheInsertAddMinutes(Addressee, code, 3);
            return temo;
        }

        [HttpGet(Name = "SendOrderEmail")]
        public async Task<ResultJson> SendOrderEmail(String orderid)
        {

            // 1、通过数据库查询得到订单号的内容信息
            var ordermes = _orderService.GetTbOrderByOrderid(orderid);
            if (ordermes.Count == 0)
                return ResultTool.Fail("没有找到");
            // 2、发送订单邮件
            return ResultTool.Success(await _emailService.SendOrderEmail(ordermes[0]));
        }

    }
}
