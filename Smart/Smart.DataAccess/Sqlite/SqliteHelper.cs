﻿using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace Smart.Core.DataAccess.Sqlite
{
    public class SqliteHelper : IDbHelper
    {
        private readonly SQLiteConnection _connection;

        public SqliteHelper( IDbConnection connection )
        {
            this._connection = ( SQLiteConnection )connection;
        }

        #region 事务
        /// <summary>
        /// 开始一个事务
        /// </summary>
        public DbTransaction BeginTractionand()
        {
            DbTransaction transaction = SqliteOperator.BeginTransaction(this._connection);
            return transaction;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTractionand( DbTransaction dbTransaction )
        {
            SqliteOperator.EndTransactionCommit( ( SQLiteTransaction )dbTransaction );
        }

        /// <summary>
        /// 结束并确认事务
        /// </summary>
        public void CommitTractionand( DbTransaction dbTransaction )
        {
            SqliteOperator.EndTransactionCommit( ( SQLiteTransaction )dbTransaction );
        }
        #endregion

        #region DataSet
        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public DataSet ExecuteDataSet( string commandText, CommandType commandType )
        {
            DataSet ds = SqliteOperator.ExecuteDataset(this._connection, commandType, commandText);
            return ds;
        }

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public DataSet ExecuteDataSet( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            DataSet ds = SqliteOperator.ExecuteDataset(this._connection, commandType, commandText, (SQLiteParameter[])parameterValues);
            return ds;
        }


        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery( string commandText, CommandType commandType )
        {
            int result = SqliteOperator.ExecuteNonQuery(this._connection, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery( DbTransaction trans, string commandText, CommandType commandType )
        {
            int result = SqliteOperator.ExecuteNonQuery((SQLiteTransaction)trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            int result = SqliteOperator.ExecuteNonQuery(this._connection, commandType, commandText, (SQLiteParameter[])parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery( DbTransaction trans, string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            int result = SqliteOperator.ExecuteNonQuery((SQLiteTransaction)trans, commandType, commandText, (SQLiteParameter[])parameterValues);
            return result;
        }
        #endregion

        #region IDataReader
        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="commandText">sql语句</param>
        public IDataReader ExecuteReader( string commandText, CommandType commandType )
        {
            IDataReader dr = SqliteOperator.ExecuteReader(this._connection, commandType, commandText);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary> 
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public IDataReader ExecuteReader( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            IDataReader dr = SqliteOperator.ExecuteReader(this._connection, commandType, commandText, (SQLiteParameter[])parameterValues);
            return dr;
        }


        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar( string commandText, CommandType commandType )
        {
            object result = SqliteOperator.ExecuteScalar(this._connection, commandType, commandText);
            return result;
        }


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            object result = SqliteOperator.ExecuteScalar(this._connection, commandType, commandText, (SQLiteParameter[])parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar( DbTransaction trans, string commandText, CommandType commandType )
        {
            object result = SqliteOperator.ExecuteScalar((SQLiteTransaction)trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar( DbTransaction trans, string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            object result = SqliteOperator.ExecuteScalar((SQLiteTransaction)trans, commandType, commandText, (SQLiteParameter[])parameterValues);
            return result;
        }

        #endregion

        /// <summary>
        /// 生成分页SQL语句
        /// </summary>
        /// <param name="pageIndex">page索引</param>
        /// <param name="pageSize">page大小</param>
        /// <param name="selectSql">查询语句</param>
        /// <param name="sqlCount">查询总数的语句</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public string GetPagingSql( int pageIndex, int pageSize, string selectSql, string orderBy )
        {
            return PageHelper.GetPagingSql( pageIndex, pageSize, selectSql, orderBy );
        }


    }
}
