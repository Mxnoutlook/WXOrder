using System;
using System.Collections.Generic;
using System.Linq;
using WxAppWebApi.Comons.Models;
using SqlSugar;
namespace WxAppWebApi.Entity
{
    /// <summary>
    /// 季丰邮箱组织表
    ///</summary>
    [SugarTable("tb_gigamail_organization")]
    public class TbGigamailOrganization: BaseEntity
    {

        /// <summary>
        /// 邮箱地址
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName= "mailaddress")]
         public string Mailaddress { get; set; }


        /// <summary>
        /// 邮件名称
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="mailname")]
         public string Mailname { get; set; }



        /// <summary>
        /// 0标识人员的，1标识是邮件组的,2标识是实验室邮件组
        /// 默认值: NULL
        ///</summary>
        [SugarColumn(ColumnName = "sign")]
        public string Sign { get; set; }

    }
}
