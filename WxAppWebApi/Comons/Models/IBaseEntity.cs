namespace WxAppWebApi.Comons.Models
{
    /// <summary>
    /// 接口，添加Id的成员变量
    /// </summary>
    public interface IBaseEntity : IEntity
    {
        /// <summary>
        /// 获得 实体唯一标识，主键
        /// </summary>
        long Id { get; set; }
    }
}
