using System;
using System.Collections.Generic;
using System.Data;
using Smart.Core.DataAccess;

namespace Smart.Framework.IDAL
{
    /// <summary>
    /// 提供数据层使用，支持传入sql
    /// </summary>
    public interface IDataRepository
    {
        IDataBase Dbase { get; }

        IDbTransaction DbTransaction { get; }

        /// <summary>
        /// 根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        IEnumerable<T> Get<T>( string sql, dynamic param = null, bool buffered = true ) where T : class;

        /// <summary>
        /// 根据条件筛选数据集合（动态类型）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        IEnumerable<dynamic> Get( string sql, dynamic param = null, bool buffered = true );

        /// <summary>
        /// 根据表达式筛选（两表关联）
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
        IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>( string sql,
            Func<TFirst, TSecond, TReturn> map,
            dynamic param = null, 
            bool buffered = true,
            string splitOn = "Id", 
            int? commandTimeout = null );

        /// <summary>
        /// 根据表达式筛选（三表关联）
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
        IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>( string sql, 
            Func<TFirst, TSecond, TThird, TReturn> map,
           dynamic param = null, 
           bool buffered = true,
           string splitOn = "Id", 
           int? commandTimeout = null );

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
        IEnumerable<T> GetPage<T>( int pageIndex, 
            int pageSize, 
            out long allRowsCount,
            string sql, 
            dynamic param = null, 
            string allRowsCountSql = null, 
            dynamic allRowsCountParam = null, 
            bool buffered = true ) where T : class;

        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Int32 Execute( string sql, dynamic param = null );


        #region 存储过程
        /// <summary>
        /// 执行存储过程返回列表
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="buffered">是否缓冲</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>查询结果泛型序列</returns>
        IEnumerable<T> StoredQuery<T>(string name, object param = null, bool buffered = true,
            int? commandTimeout = null);

        /// <summary>
        /// 存储过程取值
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        object StoredScalar(string name, object param = null, int? commandTimeout = null);

        /// <summary>
        /// 存储过程取值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        T StoredScalar<T>(string name, object param = null, int? commandTimeout = null);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <param name="commandTimeout">超时时间</param>
        void StoredExecute(string name, object param = null, int? commandTimeout = null);
        #endregion
    }
}
