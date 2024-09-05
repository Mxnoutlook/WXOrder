using WxAppWebApi.Comons.AOP.Filters;
using WxAppWebApi.Comons.Extend;
using WxAppWebApi.Comons.Helpers;
using WxAppWebApi.Comons.Result;
using WxAppWebApi.Entity;
using WxAppWebApi.service.UserService;
using Masuit.Tools;
using Masuit.Tools.Net;
using Masuit.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace WxAppWebApi.Comons.Result.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [TypeFilter(typeof(RequestLoggingFilter))] // 不需要注册，在该控制器上加上拦截器
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "AutoLogin")]
        public ResultJson AutoLogin(string js_code)
        {
            var resultStr = _userService.GetOpenID(js_code);
            // 增加条件检查，如果 resultStr 为 null，可以进行相应的处理
            if (resultStr == null)
            {
                // 处理 resultStr 为 null 的情况
                return ResultTool.Fail("无法获取微信接口返回的数据");
            }
            // 进行反序列化之前再次检查 resultStr 是否为 null
            var resultJson = JsonConvert.DeserializeObject<JObject>(resultStr);
            // 3、再缓存中查找该用户的信息是否含有
            var userID = resultJson?["openid"]?.ToString(); // 使用 null 条件运算符进行安全访问
            if (userID == null || MemoryCacheHelper.GetCacheValue(userID) == null)
                return ResultTool.Fail(Resultcode.ResultCode.NotFind, "当前用户登录失效,请重新登录");
            return ResultTool.Success(_userService.GetUserByWxId(userID));
        }


        [HttpGet(Name = "VaildCount")]
        public ResultJson VaildCount(string usermail, string userpassword)
        {
            var enpass = _userService.GetUserByEmail(usermail);
            if (enpass.Count == 0)
                return ResultTool.Fail("找不到该用户");
            if (enpass[0].Userpassword != userpassword)
                return ResultTool.Fail("账户或者密码错误");
            return ResultTool.Success(enpass);
        }


        [HttpGet(Name = "RegisterUser")]
        [TypeFilter(typeof(MemoryCacheFilter))] // 缓存资源验证，根据邮箱找验证码（key-value），没有直接返回
        public ResultJson RegisterUser(string usermail, string userpassword, string vaildcode)
        {
            // 查找该用户是否注册过
            var enpass = _userService.GetUserByEmail(usermail);
            if (enpass.Count != 0)
                return ResultTool.Fail(Resultcode.ResultCode.NotFind, "该用户已经注册过了!");
            TbUser tbUser = new TbUser();
            tbUser.Usermail = usermail;
            tbUser.Userpassword = userpassword;
            _userService.RegisterUser(tbUser);
            return ResultTool.Success(_userService.GetUserByEmail(usermail));
        }


        [HttpGet(Name = "ForGetPassWord")]
        [TypeFilter(typeof(MemoryCacheFilter))] // 缓存资源验证，根据邮箱找验证码（key-value），没有直接返回
        public ResultJson ForGetPassWord(string usermail, string vaildcode)
        {
            // 1、判断该用户是否注册过
            var enpass = _userService.GetUserByEmail(usermail);
            if (enpass.Count == 0)
                return ResultTool.Fail(Resultcode.ResultCode.NotFind, "该用户还未注册！");
            // 2、返回用户的信息
            return ResultTool.Success(enpass);
        }


        [HttpGet(Name = "ChangePassword")]
        public ResultJson ChangePassword(string password, string email)
        {
            // 1、既然可以登录那就说明该用户是被注册过的

            // 2、修改密码
            if (_userService.ChangePassword(password, email).ToInt() <= 0)
                return ResultTool.Fail("更新密码失败");
            // 3、将修改之后的用户信息返回出来
            return ResultTool.Success(_userService.GetUserByEmail(email));
        }


        [HttpGet(Name = "AddAutoLogin")]
        public ResultJson AddAutoLogin(string jsCode, string email)
        {
            // 1、获得openid
            var resultStr = _userService.GetOpenID(jsCode);
            if (resultStr == null)
            {
                return ResultTool.Fail("无法获取微信接口返回的数据");
            }

            // 进行反序列化之前再次检查 resultStr 是否为 null
            var resultJson = JsonConvert.DeserializeObject<JObject>(resultStr);
            var userID = resultJson?["openid"]?.ToString(); // 使用 null 条件运算符进行安全访问

            // 2、设置openid的cache缓存
            MemoryCacheHelper.CacheInsertFromMinutes(userID, email, 3600);

            // 3、将openid存入到数据库中
            _userService.UpdateWxid(userID, email);

            // 4、根据数据库中的openid获取到本用户的信息
            return ResultTool.Success(_userService.GetUserByWxId(userID));
        }

        [HttpGet(Name = "ChangeUserName")]
        public ResultJson ChangeUserName(string userName, string email)
        {
            if (_userService.UpdateUserName(email, userName).ToInt() > 0)
                return ResultTool.Success(_userService.GetUserByEmail(email));
            return ResultTool.Fail("更新失败");
        }




    }
}
