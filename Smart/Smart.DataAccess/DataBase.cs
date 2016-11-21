using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.DataAccess
{

    /// <summary>
    ///  数据库类型
    /// </summary>
    public enum DataBaseType
    {
        SqlServer,
        MySql,
        Oracle,
        DB2,
        Sqlite,
        Postgresql
    }

    /// <summary>
    ///  数据库对象
    /// </summary>
    public class DataBase : IDataBase
    {
        #region 只读属性
        /// <summary>
        ///  数据库链接对象
        /// </summary>
        public IDbConnection Connection { get; }

        /// <summary>
        ///  数据库链接字符串
        /// </summary>
        public string ConnKey { get; }

        /// <summary>
        ///  数据库类型
        /// </summary>
        public DataBaseType DatabaseType { get; }
        #endregion

        public DataBase(DataBaseType dbType, string connKey)
        {
            this.ConnKey = connKey;
            this.DatabaseType = dbType;
            this.Connection = DbConnFactory.CreateSqlConnection(dbType, connKey);
        }


    }
}
