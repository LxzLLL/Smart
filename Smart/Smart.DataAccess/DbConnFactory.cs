using System;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;

namespace Smart.Core.DataAccess
{
    /// <summary>
    ///  数据库链接工厂
    /// </summary>
    public class DbConnFactory
    {
        /// <summary>
        /// 根据数据库类型和配置文件字符串获取连接
        /// </summary>
        public static IDbConnection CreateSqlConnection(DataBaseType dbType, string strKey)
        {
            IDbConnection connection = null;
            string strConn = ConfigurationManager.ConnectionStrings[strKey].ConnectionString;

            switch (dbType)
            {
                case DataBaseType.SqlServer:
                    connection = new System.Data.SqlClient.SqlConnection(strConn);
                    break;
                case DataBaseType.MySql:
                    //connection = new MySql.Data.MySqlClient.MySqlConnection(strConn);
                    break;
                case DataBaseType.Oracle:
                    connection = new OracleConnection( strConn);
                    //connection = new System.Data.OracleClient.OracleConnection(strConn);
                    break;
                case DataBaseType.DB2:
                    connection = new System.Data.OleDb.OleDbConnection(strConn);
                    break;
                case DataBaseType.Sqlite:
                    connection = new System.Data.SQLite.SQLiteConnection( strConn );
                    break;
                case DataBaseType.Postgresql:
                    //connection = new System.Data.SQLite.SQLiteConnection( strConn );
                    break;
            }
            if(connection == null )
            {
                throw new Exception( "数据库链接为空！" );
            }
            return connection;
        }
    }
}
