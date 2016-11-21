using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Smart.Utils
{
    /// <summary>
    /// 基本类型转换（封装了微软自带的Convert的转换）
    /// TODO：需要实现Nullable类型的转换
    /// </summary>
    public static class SConvert
    {
        /// <summary>
        /// 对象转Decimal
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal( object FieldValue )
        {
            decimal _rst = 0.0M;
            decimal.TryParse( Convert.ToString( FieldValue ), out _rst );
            return _rst;
        }
        /// <summary>
        /// 对象转Decimal，指定默认值
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal( object FieldValue, decimal DefaultValue )
        {
            decimal _rst = DefaultValue;
            decimal.TryParse( Convert.ToString( FieldValue ), out _rst );
            return _rst;
        }

        /// <summary>
        /// 对象转Int32
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public static Int32 ToInt32( object FieldValue )
        {
            try { return FieldValue is DBNull ? 0 : Convert.ToInt32( FieldValue ); }
            catch { return 0; }
        }

        /// <summary>
        /// 对象转Int32，指定默认值
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static Int32 ToInt32( object FieldValue, Int32 DefaultValue )
        {
            try { return FieldValue is DBNull ? DefaultValue : Convert.ToInt32( FieldValue ); }
            catch { return DefaultValue; }
        }

        /// <summary>
        /// 对象转Int64
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public static Int64 ToInt64( object FieldValue )
        {
            try { return FieldValue is DBNull ? 0 : Convert.ToInt64( FieldValue ); }
            catch { return 0; }
        }

        /// <summary>
        /// 对象转Int64，指定默认值
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static Int64 ToInt64( object FieldValue, Int64 DefaultValue )
        {
            try { return FieldValue is DBNull ? DefaultValue : Convert.ToInt64( FieldValue ); }
            catch { return DefaultValue; }
        }

        /// <summary>
        /// 对象转DateTime
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime( object FieldValue )
        {
            try { return FieldValue is DBNull ? DateTime.Today : Convert.ToDateTime( FieldValue ); }
            catch { return DateTime.Today; }
        }

        /// <summary>
        /// 对象转DateTime，指定默认值
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime( object FieldValue, DateTime DefaultValue )
        {
            try { return FieldValue is DBNull ? DefaultValue : Convert.ToDateTime( FieldValue ); }
            catch { return DefaultValue; }
        }

        /// <summary>
        /// 对象转Bool
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public static bool ToBoolean( object FieldValue )
        {
            try { return FieldValue is DBNull ? false : Convert.ToBoolean( FieldValue ); }
            catch { return false; }
        }

        /// <summary>
        /// 对象转Bool，指定默认值
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static bool ToBoolean( object FieldValue, bool DefaultValue )
        {
            try { return FieldValue is DBNull ? DefaultValue : Convert.ToBoolean( FieldValue ); }
            catch { return DefaultValue; }
        }

        /// <summary>
        /// 对象转String
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public static string ToString( object FieldValue )
        {
            return FieldValue is DBNull ? "" : Convert.ToString( FieldValue );
        }

        /// <summary>
        /// 对象转String，指定默认值
        /// </summary>
        /// <param name="FieldValue"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static string ToString( object FieldValue, string DefaultValue )
        {
            return FieldValue is DBNull ? DefaultValue : Convert.ToString( FieldValue );
        }

        /// <summary>
        /// 泛型数组中是否存在对象T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist">类型数组</param>
        /// <param name="value">类型对象</param>
        /// <returns></returns>
        public static bool Exists<T>( T[ ] varlist, T value )
        {
            if ( varlist.Length <= 0 ) return false;
            foreach ( T val in varlist )
            {
                if ( val.Equals( value ) )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为Email类型
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <returns></returns>
        public static bool IsEmail( string inputEmail )
        {
            if ( !string.IsNullOrEmpty( inputEmail ) )
            {
                string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(strRegex);
                return re.IsMatch( inputEmail );
            }
            return false;
        }
    }
}
