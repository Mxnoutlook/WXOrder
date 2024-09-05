using WxAppWebApi.Entity;
using System.Data;

namespace WxAppWebApi.service.UserService
{
    public interface IUserService
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string RegisterUser(TbUser user);


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public string ChangePassword(string password,string email);


        /// <summary>
        /// 通过微信ID查找人员
        /// </summary>
        /// <param name="wxid"></param>
        /// <returns></returns>
        public List<TbUser> GetUserByWxId(string wxid);


        /// <summary>
        /// 通过email查找用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<TbUser> GetUserByEmail(string email);


        /// <summary>
        /// 获得微信用户的openid和其它
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>

        public String GetOpenID(string code);


        /// <summary>
        /// 更新wxid的值
        /// </summary>
        /// <param name="wxid"></param>
        /// <returns></returns>
        public String UpdateWxid(string wxid,string email);


        /// <summary>
        /// 更新用户的名称
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public String UpdateUserName(string email, string username);



        /// <summary>
        /// 通过订单号获得公司的名称
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string GetCompNameByOrderId(string orderid);

    }
}
