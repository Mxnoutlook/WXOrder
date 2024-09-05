using WxAppWebApi.Comons.Helpers;
using WxAppWebApi.Entity;
using Masuit.Tools;
using Masuit.Tools.Database;
using Masuit.Tools.Models;
using Newtonsoft.Json;
using System.Data;

namespace WxAppWebApi.service.UserService.Impl
{
    public class UserServiceImpl : IUserService
    {
        public string ChangePassword(string password, string email)
        {

            return SqlSugarHelper.Db.Updateable<TbUser>().SetColumns(it => it.Userpassword == password).Where(it => it.Usermail == email).ExecuteCommand().ToString();
        }

        public List<TbUser> GetUserByWxId(string wxid)
        {
            var queryTable = SqlSugarHelper.Db.Queryable<TbUser, TbCompanyOrganization>((u, c) => u.Usercompany == c.Companycode && u.Wxid == wxid).Select((u, c) => new { u.Username, u.Usermail, u.Usercompany, u.Uservaildname, u.Userownmail, u.Userpassword, c.Companyname }).ToList();
            TbUser tbUser = new TbUser();
            List<TbUser> tbUsers = new List<TbUser>();
            foreach (var item in queryTable)
            {
                tbUser.Userownmail = item.Userownmail;
                tbUser.Userpassword = item.Userpassword;
                tbUser.Username = item.Username;
                tbUser.Usercompany = item.Usercompany;
                tbUser.Uservaildname = item.Uservaildname;
                tbUser.CompanyName = item.Companyname;
                tbUser.Id = 0;
                tbUsers.Add(tbUser);
            }
            return tbUsers;
        }

        public List<TbUser> GetUserByEmail(string email)
        {
            var queryTable=  SqlSugarHelper.Db.Queryable<TbUser, TbCompanyOrganization>((u, c) => u.Usercompany == c.Companycode && u.Usermail == email).Select((u, c) => new {u.Username,u.Usermail,u.Usercompany,u.Uservaildname,u.Userownmail,u.Userpassword,c.Companyname}).ToList();

            TbUser tbUser = new TbUser();
            List<TbUser> tbUsers = new List<TbUser>();
            foreach (var item in queryTable)
            {
                tbUser.Userownmail = item.Userownmail;
                tbUser.Userpassword = item.Userpassword;
                tbUser.Username = item.Username;
                tbUser.Usercompany = item.Usercompany;
                tbUser.Uservaildname = item.Uservaildname;
                tbUser.Usermail = item.Usermail;
                tbUser.CompanyName=item.Companyname;
                tbUser.Id = 0;
                tbUsers.Add(tbUser);
            }
            return tbUsers;
        }

        public string RegisterUser(TbUser user)
        {
            var recountrow = SqlSugarHelper.Db.Insertable(user).ExecuteCommand();
            return recountrow.ToString();
        }

        public string GetOpenID(string code)
        {
            // 1、第一次登录获得wx调用的值
            var appid = "wx51975ebba7a10b27";
            var secret = "de66af60a5e3a2b173f761ca98a4f692";

            // 2、调用微信的接口
            RestClientHepler restClient = new RestClientHepler($"https://api.weixin.qq.com/sns/jscode2session");
            return  restClient.Get($"appid={appid}&secret={secret}&js_code={code}&grant_type=authorization_code");
        }
        public string UpdateWxid(string wxid, string email)
        {
            return SqlSugarHelper.Db.Updateable<TbUser>().SetColumns(it => it.Wxid == wxid).Where(it => it.Usermail == email).ExecuteCommand().ToString();
        }

        public string UpdateUserName(string email, string username)
        {
            return SqlSugarHelper.Db.Updateable<TbUser>().SetColumns(it => it.Username == username).Where(it => it.Usermail == email).ExecuteCommand().ToString();
        }

        public string GetCompNameByOrderId(string orderid)
        {
            var resultdata = SqlSugarHelper.Db.Ado.GetDataTable($"SELECT co.companyname,u.usermail FROM tb_company_organization co JOIN tb_user u ON u.usercompany=co.companycode JOIN tb_order c ON u.usermail=c.usercode WHERE c.orderNum='{orderid}'").ToJsonString();
            var jsonobject = JsonConvert.DeserializeObject<dynamic>(resultdata);
            return  $"{Convert.ToString(jsonobject[0]["companyname"])}//{Convert.ToString(jsonobject[0]["usermail"])}";

        }
    }
}
