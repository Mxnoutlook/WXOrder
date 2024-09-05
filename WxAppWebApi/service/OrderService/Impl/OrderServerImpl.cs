using WxAppWebApi.Comons.Helpers;
using WxAppWebApi.Comons.Result;
using WxAppWebApi.Entity;
using MailKit.Search;
using Masuit.Tools.Reflection;
using SqlSugar;

namespace WxAppWebApi.service.OrderService.Impl
{
    public class OrderServerImpl : IOrderService
    {
        public ResultJson AddAndUpdateOrder(TbOrder tbOrder)
        {
            // 1、设置一个订单号码，使用全局唯一ID作为一个订单号
            tbOrder.OrderNum = IdGeneratorHelper.GuId();
            // 2、设置下单事件
            tbOrder.CreateTime = DateTime.Now;
            tbOrder.Sign = "0";

            if (SqlSugarHelper.Db.Insertable(tbOrder).ExecuteCommand() > 0)
                return ResultTool.Success(tbOrder.OrderNum);
            return ResultTool.Fail("下单失败");
        }

        public string DeleteOrderRecordAndFileByOrderid(string orderid)
        {
            // 1、先查找该记录，然后将记录中的数据某一字段拿到
            var filepaths = SqlSugarHelper.Db.Queryable<TbOrder>().Where(o => o.OrderNum == orderid).Select(o => o.Filepath).First();

            string[] files = filepaths.Split(';');
            foreach (string filepath in files)
            {
                if (filepath != "")
                    File.Delete(filepath);
            }

            // 根据orderid删除文件和文件记录
            if (SqlSugarHelper.Db.Deleteable<TbOrder>().Where(it => it.OrderNum == orderid).ExecuteCommand() > 0)
                return "删除成功";
            return "删除失败";

        }

        public List<TbGigamailOrganization> getAllGfMail()
        {
            return SqlSugarHelper.Db.Queryable<TbGigamailOrganization>().ToList();
        }

       
        public List<TbOrder> GetTbOrderByOrderid(string orderid)
        {
             return SqlSugarHelper.Db.Queryable<TbOrder>().Where(it => it.OrderNum == orderid).ToList();
        }

        public List<TbCompmailOrganization> GetUserCompanyMail(string companycode)
        {
            return SqlSugarHelper.Db.Queryable<TbCompmailOrganization>().Where(it => it.Companycode == companycode).ToList();
        }




        public string UpdateOrderFilePath(string filepath, string orderid)
        {
            // 先根据orderid 找到路径字段 取出值a
            var originalPath = SqlSugarHelper.Db.Queryable<TbOrder>().Where(o => o.OrderNum == orderid).Select(o => o.Filepath).First();
            if (!string.IsNullOrEmpty(originalPath))
                originalPath += ";" + filepath;
            else
                originalPath = filepath;

            // 更新数据库中的路径字段
            if (SqlSugarHelper.Db.Updateable<TbOrder>().SetColumns(o => o.Filepath == originalPath).Where(o => o.OrderNum == orderid).ExecuteCommand() > 0)
                return "更新成功";
            return "更新失败";
        }
    }
}
