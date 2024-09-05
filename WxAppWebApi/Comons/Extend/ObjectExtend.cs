using System.Data;

namespace WxAppWebApi.Comons.Extend
{
    /// <summary>
    /// 对象自定义扩展类
    /// </summary>
    public static class ObjectExtend
    {
        /// <summary>
        /// 将object转换为long类型信息。不能接收非数字字符，否则将会转换失败。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默认值。</param>
        /// <returns>long。</returns>
        public static long ToLong(this object o, long t = default(long))
        {
            if (o == null)
            {
                return t; // 如果 o 为空，返回默认值 t
            }

            string stringValue = o.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(stringValue))
            {
                return t; // 如果 stringValue 为空，返回默认值 t
            }

            long info;


            if (!long.TryParse(stringValue, out info))
            {
                info = t;
            }
            return info;
        }




    }
}
