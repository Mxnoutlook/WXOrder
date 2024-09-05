using System;
using System.Collections.Generic;
using System.Linq;
using WxAppWebApi.Comons.Models;
using SqlSugar;
namespace WxAppWebApi.Entity
{
    /// <summary>
    /// 公司邮箱组织表
    ///</summary>
    [SugarTable("tb_compmail_organization")]
    public class TbCompmailOrganization: BaseEntity
    {
       
        /// <summary>
        /// 公司邮箱 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="companymail"    )]
         public string Companymail { get; set; }
        /// <summary>
        /// 公司编码 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="companycode"    )]
         public string Companycode { get; set; }

        /// <summary>
        /// 邮件的名称
        /// </summary>
        [SugarColumn(ColumnName ="mailname")]
        public string Mailname { get; set; }


        /// <summary>
        /// 标识内容，0标识人员，1标识邮件组
        /// </summary>
        [SugarColumn(ColumnName ="sign")]
        public string Sign { get; set; }
    }
}
