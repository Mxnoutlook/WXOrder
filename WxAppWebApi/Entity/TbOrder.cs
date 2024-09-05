using System;
using System.Collections.Generic;
using System.Linq;
using WxAppWebApi.Comons.Models;
using SqlSugar;
namespace WxAppWebApi.Entity
{
    /// <summary>
    /// 用户下单表
    ///</summary>
    [SugarTable("tb_order")]
    public class TbOrder: BaseEntity
    {
  
        /// <summary>
        /// 下单编码 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="orderNum")]
         public string? OrderNum { get; set; }
        /// <summary>
        /// 委托实验室 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="orderLab")]
         public string? OrderLab { get; set; }
        /// <summary>
        /// 收件人 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="addressee")]
         public string? Addressee { get; set; }
        /// <summary>
        /// 抄送人 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="ccpeople")]
         public string? Ccpeople { get; set; }
        /// <summary>
        /// 取件时间 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="pickuptime")]
         public DateTime? Pickuptime { get; set; }
        /// <summary>
        /// 下单时间 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="create_time")]
         public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 文件路径 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="filepath")]
         public string? Filepath { get; set; }
        /// <summary>
        /// 备注内容 
        /// 默认值: NULL
        ///</summary>
         [SugarColumn(ColumnName="mailbody")]
         public string? Mailbody { get; set; }


        /// <summary>
        /// 下单邮件的主题
        /// </summary>
        [SugarColumn(ColumnName = "orderTitle")]
        public string? Ordertitle { get; set; }


        /// <summary>
        /// 0表示是草稿箱的，1表示是已经发送的
        /// </summary>
        [SugarColumn(ColumnName ="sign")]
        public string? Sign { get; set; }

        /// <summary>
        /// 表示是谁下单的
        /// </summary>
        [SugarColumn(ColumnName ="usercode")]
        public string? Usercode { get; set; }

    }
}
