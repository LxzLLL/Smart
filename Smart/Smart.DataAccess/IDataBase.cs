using System.Data;

namespace Smart.Core.DataAccess
{
    /// <summary>
    ///  数据库接口
    /// </summary>
    public interface IDataBase
    {
        /// <summary>
        ///  数据库链接
        /// </summary>
        IDbConnection Connection { get; }
        /// <summary>
        ///  数据库类型
        /// </summary>
        DataBaseType DatabaseType { get; }
        /// <summary>
        ///  数据库链接字符串
        /// </summary>
        string ConnKey { get; }
    }
}
