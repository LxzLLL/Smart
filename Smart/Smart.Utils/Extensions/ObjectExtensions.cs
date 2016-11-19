using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.Utilities
{
    /// <summary>
    /// 提供一组对 基础对象类型 <see cref="System.Object"/> 操作方法的扩展。
    /// </summary>
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 判断指定的对象是否为 Null 或者等于 DBNull.Value 值。
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static bool IsNull(this Object _this)
        {
            return _this == null || Convert.IsDBNull(_this);
        }
    }
}
