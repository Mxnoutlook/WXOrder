using WxAppWebApi.Comons.Models;
using SqlSugar;
namespace WxAppWebApi.Entity
{
    /// <summary>
    /// 登录用户的表
    ///</summary>
    [SugarTable("user_mes", "用户信息表")]
    public class UserMes : BaseEntity
    {
        public UserMes()
        {

        }
        /// <summary>
        /// 用户名 
        /// 默认值: NULL
        ///</summary>
        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户性别 
        /// 默认值: NULL
        ///</summary>
        [SugarColumn(ColumnName = "user_sex")]
        public string UserSex { get; set; }
        /// <summary>
        /// 用户语言 
        /// 默认值: NULL
        ///</summary>
        [SugarColumn(ColumnName = "user_language")]
        public string UserLanguage { get; set; }
    }
}
