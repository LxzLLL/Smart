using System.Collections.Generic;
using System.Linq;

namespace Smart.Core.Utils
{
    /// <summary>
    /// 提供一组对 公开泛型枚举数类型 <see cref="System.Collections.Generic.IEnumerable&lt;TSource&gt;"/> 操作方法的扩展。
    /// </summary>
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// 判断指定的序列对象 <paramref name="_this"/> 是否为 Null 或不包含任何元素。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="_this">被判断的序列 <see cref="IEnumerable"/> 对象。</param>
        /// <returns>如果序列对象 <paramref name="_this"/> 为 Null 或者不包含任何元素，则返回 true；否则返回 false。</returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> _this)
        {
            return _this == null || _this.Count() == 0;
        }
    }
}
