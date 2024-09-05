using SqlSugar;

namespace WxAppWebApi.Entity
{
    /// <summary>
    /// 用户规则信息
    /// </summary>   
    [SugarTable("tb_role")]
    public class UserRole {

        [SugarColumn(ColumnName = "id")]
        public string id { get; set; }

        [SugarColumn(ColumnName = "role_name")]
        public string role_name { get; set; }

        [SugarColumn(ColumnName = "role_action")]
        public string role_action { get; set; }

        [SugarColumn(ColumnName = "role_module")]
        public string role_module { get; set; }

        [SugarColumn(ColumnName = "role_view")]
        public string role_view { get; set; }
    }
}
