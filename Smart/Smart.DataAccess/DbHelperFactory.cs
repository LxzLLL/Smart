/****************************************************************
 * 目的：上层Business层业务使用此方法获取对应的数据库帮助类
 * 
 * 
 * **************************************************************/

using Smart.DataAccess.Oracle;
using Smart.DataAccess.SqlServer;

namespace Smart.DataAccess
{
    public class DbHelperFactory
    {
        /// <summary>
        /// 根据IDatabase接口，获取IDbHelper
        /// </summary>
        public static IDbHelper GetDbAdaptor( IDataBase database )
        {
            IDbHelper adaptor = null;
            switch ( database.DatabaseType )
            {
                case DataBaseType.SqlServer:
                    adaptor = new SqlHelper( database.Connection );
                    break;
                case DataBaseType.MySql:
                    //adaptor = new MySqlDBHelper.MySqlAdaptor( database.Connection );
                    break;
                case DataBaseType.Oracle:
                    adaptor = new OracleHelper( database.Connection );
                    break;
                case DataBaseType.DB2:
                //adaptor = new MySqlDBHelper.MySqlAdaptor( database.Connection );
                    break;
                case DataBaseType.Sqlite:
                    //adaptor = new SqliteDBHelper.SqliteAdaptor( database.Connection );
                    break;
            }
            return adaptor;
        }
    }
}
