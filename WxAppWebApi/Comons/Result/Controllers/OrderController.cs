using WxAppWebApi.Comons.AOP.Filters;
using WxAppWebApi.Comons.Result;
using WxAppWebApi.Entity;
using WxAppWebApi.service.OrderService;
using Masuit.Tools;
using Masuit.Tools.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WxAppWebApi.service.UserService;

namespace WxAppWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [TypeFilter(typeof(RequestLoggingFilter))]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService,IUserService userService)
        {
            _orderService = orderService;
            _userService= userService;
        }

        [HttpGet(Name = "GetGfMail")]
        public ResultJson GetGfMail()
        {
            var resultdata = _orderService.getAllGfMail();
            if (resultdata.Count == 0)
                return ResultTool.Fail("没有找到本公司的邮件信息");
            return ResultTool.Success(resultdata);
        }

        [HttpGet(Name = "GetUserCompanyMail")]
        public ResultJson GetUserCompanyMail(string compcode)
        {
            var resultdata = _orderService.GetUserCompanyMail(compcode);
            if (resultdata.Count == 0)
                return ResultTool.Fail("没有找到该公司的邮件信息");
            return ResultTool.Success(resultdata);
        }

        [HttpGet(Name = "GetAllMail")]
        public ResultJson GetAllMail(string compcode)
        {
            // 1、先获得季丰公司和用户公司的信息
            var userMail = _orderService.GetUserCompanyMail(compcode).ToList();
            var gfMail = _orderService.getAllGfMail().ToList();

            // 2、合并两个数组，遍历区分，分为实验室邮件组和用户组
            var allEmails = new List<object>();
            allEmails.AddRange(userMail);
            allEmails.AddRange(gfMail);

            // 3、区分实验室邮件组和用户组
            var labEmails = new List<object>();
            var userGroupEmails = new List<object>();

            foreach (var emailEntity in allEmails)
            {
                // 在这里添加逻辑以区分实验室邮件组和用户组
                if (emailEntity.GetType().Name == "TbCompmailOrganization")
                {
                    var userCompany = emailEntity as TbCompmailOrganization;
                    userGroupEmails.Add($"{userCompany.Mailname},<{userCompany.Companymail}>");
                }
                else
                {
                    var gfCompany = emailEntity as TbGigamailOrganization;
                    if (gfCompany.Sign == "2")
                        labEmails.Add($"{gfCompany.Mailname},<{gfCompany.Mailaddress}>");
                    userGroupEmails.Add($"{gfCompany.Mailname},<{gfCompany.Mailaddress}>");
                }
            }

            // 4、拼接成 JSON 数据返回
            var resultData = new
            {
                LabEmails = labEmails,
                UserGroupEmails = userGroupEmails
            };


            // 5、拼接成为一个 JSON 数据返回
            return ResultTool.Success(resultData);
        }



        [HttpPost(Name = "AddOrder")]

        public ResultJson AddOrder(TbOrder ordermes)
        {
            // 1、下单，筛选出其中没有的邮件
            



            // 2、将邮件拼接成为某种形式然后添加进去

            return _orderService.AddAndUpdateOrder(ordermes);
        }


        [HttpGet(Name = "DeleteOrder")]
        public ResultJson DeleteOrder(string ordercode)
        {
            // 1、先根据这个下单的编号查找下单的记录并且删除该记录
            return ResultTool.Success(_orderService.DeleteOrderRecordAndFileByOrderid(ordercode));

        }
    }
}







    