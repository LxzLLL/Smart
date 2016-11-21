/************************************************************************
 * 创  建 人：ArvinLv
 * 创建时间：2016年11月17日23:14:09
 * 创建目的：使用传统的ADO.NET开发方式，方便一些喜欢使用DataSet的开发人员，此接口为抽象
 *               数据库操作接口，所有类型的数据库都继承此接口
 *  TODO：1、需要记录sql的执行时间（在执行数据库时捕获），且需要记录到日志中（使用log4net）；2、处理带隔离级别的事务
 *  
 ************************************************************************/

using System.Data.Common;
using System.Data;

namespace Smart.Core.DataAccess
{
    /// <summary>
    /// 提供对数据库的基本操作，连接字符串需要在数据库配置。
    /// </summary>
    public interface IDbHelper
    {
        /// <summary>
        /// 生成分页SQL语句
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selectSql"></param>
        /// <param name="sqlCount"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        string GetPagingSql(int pageIndex, int pageSize, string selectSql, string orderBy);

        #region 事务
        /// <summary>
        /// 开始一个事务
        /// </summary>
        /// <returns></returns>
        DbTransaction BeginTractionand();
        
        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="dbTransaction">要回滚的事务</param>
        void RollbackTractionand(DbTransaction dbTransaction);
        
        /// <summary>
        /// 结束并确认事务
        /// </summary>
        /// <param name="dbTransaction">要结束的事务</param>
        void CommitTractionand(DbTransaction dbTransaction);
        #endregion

        #region 返回DataSet

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        DataSet ExecuteDataSet(string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        DataSet ExecuteDataSet(string commandText, CommandType commandType, params DbParameter[] parameterValues);



        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        int ExecuteNonQuery(string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        int ExecuteNonQuery(DbTransaction trans, string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        int ExecuteNonQuery(string commandText, CommandType commandType, params DbParameter[] parameterValues);

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        int ExecuteNonQuery(DbTransaction trans, string commandText, CommandType commandType, params DbParameter[] parameterValues);

        #endregion

        #region IDataReader

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        IDataReader ExecuteReader(string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary> 
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        IDataReader ExecuteReader(string commandText, CommandType commandType, params DbParameter[] parameterValues);

        #endregion

        #region ExecuteScalar返回第一行第一列的值

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        object ExecuteScalar(string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        object ExecuteScalar(string commandText, CommandType commandType, params DbParameter[] parameterValues);

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        object ExecuteScalar(DbTransaction trans, string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        object ExecuteScalar(DbTransaction trans, string commandText, CommandType commandType, params DbParameter[] parameterValues);
        #endregion
    }
}