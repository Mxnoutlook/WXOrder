using WxAppWebApi.Comons.Result;
using WxAppWebApi.Entity;

namespace WxAppWebApi.service.OrderService
{
    public interface IOrderService
    {
        /// <summary>
        /// 获得所有的季丰的邮箱
        /// </summary>
        /// <returns></returns>
        public List<TbGigamailOrganization> getAllGfMail();



        /// <summary>
        /// 通过公司编号获得公司的人员邮件
        /// </summary>
        /// <param name="companycode"></param>
        /// <returns></returns>
        public List<TbCompmailOrganization> GetUserCompanyMail(string companycode);


        /// <summary>
        /// 根据传来的数据插入获得更新ORDER
        /// </summary>
        /// <param name="tbOrder"></param>
        /// <returns></returns>
        public ResultJson AddAndUpdateOrder(TbOrder tbOrder);

        /// <summary>
        /// 更新文件路径，通过下单ID
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public String UpdateOrderFilePath(string filepath, string orderid);




        /// <summary>
        /// 根据orderid删除文件和文件记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string DeleteOrderRecordAndFileByOrderid(string orderid);



        /// <summary>
        /// 通过订单号获得订单的信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<TbOrder> GetTbOrderByOrderid(string orderid);

    }
}
