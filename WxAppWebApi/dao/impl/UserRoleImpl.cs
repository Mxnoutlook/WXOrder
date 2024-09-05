using WxAppWebApi.Comons.Helpers;
using WxAppWebApi.Entity;

namespace WxAppWebApi.dao.impl
{
    public class UserRoleImpl : IUserRoleDao {
      

        List<UserRole> IUserRoleDao.getAllUserRole() {
            
            return SqlSugarHelper.Db.Queryable<UserRole>().ToList();
        }

        List<UserMes> IUserRoleDao.getUserMes()
        {
            return SqlSugarHelper.Db.Queryable<UserMes>().ToList();
        }
    }
}
