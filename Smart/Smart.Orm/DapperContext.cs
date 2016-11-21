using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Smart.Core.DataAccess;
using Smart.Framework.IDAL;
using Smart.Core.Utils;

namespace Smart.Core.Orm
{

    /// <summary>
    /// Dapper的数据库上下文类，实现IDisposable接口，
    /// GC自动回收（可能释放资源较慢，可以在上层调用处使用using或手动关闭连接）
    /// </summary>
    public class DapperContext: IDataRepository,IDisposable
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        private IDataBase _dataBase;

        /// <summary>
        /// 数据库事务
        /// </summary>
        private IDbTransaction _dbTransaction;
        
        public DapperContext( IDataBase dataBase, IDbTransaction dbTransaction=null )
        {
            this._dataBase = dataBase;
            this._dbTransaction = dbTransaction;
        }


        #region 事务
        /// <summary>
        ///  开始事务
        /// </summary>
        /// <param name="isolation">事务隔离级别，默认为ReadCommited</param>
        public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            this.TryOpenConnection();
            this.BeginTransaction( this._dataBase.Connection.BeginTransaction( isolation ) );
        }

        /// <summary>
        /// 开始事务（带事务对象）
        /// </summary>
        /// <param name="dbTransaction">事务对象</param>
        /// 
        public void BeginTransaction( IDbTransaction dbTransaction, IsolationLevel isolation = IsolationLevel.ReadCommitted )
        {
            this.TryOpenConnection();
            this._dbTransaction = dbTransaction;
            this._dataBase.Connection.BeginTransaction(isolation);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if ( this._dbTransaction != null )
            {
                this._dbTransaction.Commit();
                //Commit之后虽会将事务对象的连接信息清空，但对象本身仍旧存在。为方便外部获取事务对象后判定空，此处清空事务对象。
                this._dbTransaction = null;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if ( this._dbTransaction != null )
            {
                this._dbTransaction.Rollback();
                //Rollback之后虽会将事务对象的连接信息清空，但对象本身仍旧存在。为方便外部获取事务对象后判定空，此处清空事务对象。
                this._dbTransaction = null;
            }
        }
        #endregion

        #region IDataRepository成员
        /// <summary>
        ///  数据库对象
        /// </summary>
        public IDataBase Dbase { get { return this._dataBase; } }
        public IDbTransaction DbTransaction { get { return this._dbTransaction; } }

        /// <summary>
        /// 根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        public IEnumerable<T> Get<T>( string sql, dynamic param = null,  bool buffered = true ) where T : class
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.Query<T>( sql, param as object, this._dbTransaction, buffered );
        }

        /// <summary>
        /// 根据条件筛选数据集合（动态类型）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        public IEnumerable<dynamic> Get( string sql, dynamic param = null,  bool buffered = true )
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.Query( sql, param as object, this._dbTransaction, buffered );
        }

        /// <summary>
        /// 分页查询
        /// TODO：需重写page方法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="allRowsCount">全部数据总数</param>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="allRowsCountSql">获取全部数据总数的SQL</param>
        /// <param name="allRowsCountParam">参数</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>( int pageIndex, int pageSize, out long allRowsCount, string sql, dynamic param = null, string allRowsCountSql = null, dynamic allRowsCountParam = null, bool buffered = true ) where T : class
        {
            this.TryOpenConnection();
            //IEnumerable<T> entityList = Dbase.Connection.GetPage<T>(pageIndex, pageSize, out allRowsCount, sql, param as object, allRowsCountSql, null, null, buffered, databaseType: Dbase.DatabaseType);
            //return entityList;
            allRowsCount = 0;
            return new List<T>();
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>( string sql, Func<TFirst, TSecond, TReturn> map,
            dynamic param = null,  bool buffered = true, string splitOn = "Id",int? commandTimeout = null )
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.Query( sql, map, param as object, this._dbTransaction, buffered, splitOn, commandTimeout );
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>( string sql, Func<TFirst, TSecond, TThird, TReturn> map,
            dynamic param = null,  bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null )
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.Query( sql, map, param as object, this._dbTransaction, buffered, splitOn, commandTimeout );
        }

        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute( string sql, dynamic param = null )
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.Execute( sql, param as object, this._dbTransaction );
        }


        /// <summary>
        /// 执行存储过程返回列表
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="buffered">是否缓冲</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>查询结果泛型序列</returns>
        public IEnumerable<T> StoredQuery<T>( string name, object param = null, bool buffered = true, int? commandTimeout = null )
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.Query<T>( name, param, this._dbTransaction, buffered, commandTimeout, CommandType.StoredProcedure );
        }

        /// <summary>
        /// 存储过程取值
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public object StoredScalar( string name, object param = null, int? commandTimeout = null )
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.ExecuteScalar( name, param, this._dbTransaction, commandTimeout, CommandType.StoredProcedure );
        }

        /// <summary>
        /// 存储过程取值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public T StoredScalar<T>( string name, object param = null, int? commandTimeout = null )
        {
            this.TryOpenConnection();
            return this._dataBase.Connection.ExecuteScalar<T>( name, param, this._dbTransaction, commandTimeout, CommandType.StoredProcedure );
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="commandTimeout">超时时间</param>
        public void StoredExecute( string name, object param = null, int? commandTimeout = null )
        {
            this.TryOpenConnection();
            this._dataBase.Connection.Execute( name, param, this._dbTransaction, commandTimeout, CommandType.StoredProcedure );
        }

        #endregion



        /// <summary>
        /// 尝试打开数据库连接
        /// </summary>
        private void TryOpenConnection()
        {
            if ( this._dataBase.Connection.State == ConnectionState.Closed )
            {
                try { this._dataBase.Connection.Open(); }
                catch ( Exception e )
                {
                    throw ExceptionHelper.ThrowDataAccessException( "Dapper打开数据库连接时发生异常。", e );
                }
            }
        }

        #region 释放资源

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                //事务析构
                if (this._dbTransaction != null)
                {
                    try
                    {
                        this._dbTransaction.Dispose();
                        this._dbTransaction = null;
                    }
                    catch
                    {
                    }
                }
                //连接关闭及析构
                if (this._dataBase.Connection != null)
                {
                    try
                    {
                        this._dataBase.Connection.Close();
                        this._dataBase.Connection.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
        }

        ~DapperContext() { Dispose( false ); }

        #endregion
    }
}
