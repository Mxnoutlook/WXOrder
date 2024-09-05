
using WxAppWebApi.Entity;

namespace WxAppWebApi.dao {
    /// <summary>
    /// 接口的DAO层，写方法
    /// </summary>
    public interface IUserRoleDao {

        List<UserRole> getAllUserRole();

        List<UserMes> getUserMes();



    }
}
