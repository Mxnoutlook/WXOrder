using System;
using System.Collections.Generic;
using System.Linq;
using WxAppWebApi.Comons.Models;
using SqlSugar;
namespace WxAppWebApi.Entity
{
    /// <summary>
    /// 用户表
    ///</summary>
    [SugarTable("tb_user")]
    public class TbUser: BaseEntity
    {
        /// <summary>
        /// 用户名（微信昵称） 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="username"    )]
         public string? Username { get; set; }
        /// <summary>
        /// 用户邮箱地址 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="usermail"    )]
         public string? Usermail { get; set; }
        /// <summary>
        /// 登录密码，MD5加密 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="userpassword"    )]
         public string? Userpassword { get; set; }
        /// <summary>
        /// 用户所属公司ID 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="usercompany"    )]
         public string? Usercompany { get; set; }
        /// <summary>
        /// 用户真实姓名 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="uservaildname"    )]
         public string? Uservaildname { get; set; }
        /// <summary>
        /// 用户的历史邮件 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="userownmail")]
         public string? Userownmail { get; set; }

        /// <summary>
        /// 用于标识微信的唯一身份
        /// </summary>
        [SugarColumn(ColumnName ="wxid")]
        public string? Wxid { get; set; }


        public string? CompanyName { get; set; }

    }
}
