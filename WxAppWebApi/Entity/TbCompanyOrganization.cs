using System;
using System.Collections.Generic;
using System.Linq;
using WxAppWebApi.Comons.Models;
using SqlSugar;
namespace WxAppWebApi.Entity
{
    /// <summary>
    /// 公司组织表
    ///</summary>
    [SugarTable("tb_company_organization")]
    public class TbCompanyOrganization:BaseEntity
    {
     
        /// <summary>
        /// 公司的编号 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="companycode"    )]
         public string Companycode { get; set; }
        /// <summary>
        /// 公司的名称 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="companyname"    )]
         public string Companyname { get; set; }
    }
}
