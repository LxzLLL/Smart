using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Smart.Core.Utils
{
	/// <summary>
	/// web.config操作类
	/// </summary>
	public sealed class ConfigHelper
	{
        /// <summary>
        /// 得到AppSettings中的配置字符串信息，并加入缓存中（名为"AppSettings_" + key）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
		{
            string CacheKey = "AppSettings_" + key;
            object objModel = CacheHelper.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key]; 
                    if (objModel != null)
                    {
                        CacheHelper.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
		}

		/// <summary>
		/// 得到AppSettings中的配置Bool信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetConfigBool(string key)
		{
			bool result = false;
			string cfgVal = GetConfigString(key);
			if(!string.IsNullOrEmpty(cfgVal))
			{
				try
				{
					result = bool.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}
			return result;
		}

		/// <summary>
		/// 得到AppSettings中的配置Decimal信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static decimal GetConfigDecimal(string key)
		{
			decimal result = 0;
			string cfgVal = GetConfigString(key);
			if(!string.IsNullOrEmpty(cfgVal))
			{
				try
				{
					result = decimal.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
		}

		/// <summary>
		/// 得到AppSettings中的配置int信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static int GetConfigInt(string key)
		{
			int result = 0;
			string cfgVal = GetConfigString(key);
			if(!string.IsNullOrEmpty(cfgVal))
			{
				try
				{
					result = int.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
		}


        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ConnectionString( string key )
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[ key ].ConnectionString;
            }
            catch
            {
                throw ExceptionHelper.ThrowConfigException( "不存在名为“" + key + "”的数据库连接信息，请检查web.config文件中的设置。" );
            }
        }

        /// <summary>
        /// 提供程序名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>i
        public static string ProviderName( string key )
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[ key ].ProviderName;
            }
            catch
            {
                throw ExceptionHelper.ThrowConfigException( "不存在名为“" + key + "”的数据库连接信息，请检查web.config文件中的设置。" );
            }
        }

        /// <summary>
        /// 尝试获取指定名称的数据库连接信息，若该连接信息存在则返回 true，并以输出参数的方式返回数据库连接字符串和数据库管道名称，若该连接信息不存在则返回 false。
        /// </summary>
        /// <param name="key">指定的数据库连接名称</param>
        /// <param name="connectionString">输出参数，表示获取到的数据库连接字符串</param>
        /// <param name="providerName">输出参数，表示获取到的数据库管道名称</param>
        /// <returns></returns>
        public static bool TryGetConnectionInfo( string key, out string connectionString, out string providerName )
        {
            var target = ConfigurationManager.ConnectionStrings[key];

            if ( target != null )
            {
                connectionString = target.ConnectionString;
                providerName = target.ProviderName;
                return true;
            }
            else
            {
                connectionString = string.Empty;
                providerName = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// 配置文件设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AppSetting( string key )
        {
            var set = ConfigurationManager.AppSettings[key];

            if ( set != null ) { return set; }
            throw ExceptionHelper.ThrowConfigException( "不存在名为“" + key + "”的AppSetting配置信息，请检查相应配置文件中的设置。" );
        }

        /// <summary>
        /// 尝试获取指定名称的AppSetting配置信息，若该信息存在则返回 true，并以输出参数的方式返回其值，若该信息不存在则返回 false。
        /// </summary>
        /// <param name="key">指定的配置名称</param>
        /// <param name="value">输出参数，表示获取到的值</param>
        /// <returns></returns>
        public static bool TryGetAppSettingInfo( string key, out string value )
        {
            var target = ConfigurationManager.AppSettings[key];

            if ( !string.IsNullOrEmpty( target ) )
            {
                value = target;
                return true;
            }
            else
            {
                value = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// 解析指定的数据库连接字符串，并以键值对方式返回其中的 属性名和属性值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>一个键值对集合，其中 key 为属性名， value 为属性值。</returns>
        public static Dictionary<string, string> ParseConnectionString( string connectionString )
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string[] arr = connectionString.ToArray(";", true);
            foreach ( var item in arr )
            {
                string[] temp = item.ToArray("=", 2, false);
                if ( temp.Length == 2 )
                {
                    string key = temp[0].Trim(), value = temp[1].Trim();
                    if ( !result.ContainsKey( key ) )
                    { result.Add( key, value ); }
                    else
                    { result[ key ] = value; }
                }
            }

            return result;
        }

        /// <summary>
        /// 解析指定的数据库连接字符串，返回不带安全凭据信息的数据库连接字符串值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>不带安全凭据信息的数据库连接字符串值</returns>
        public static string GetConnectionStringWithoutCredentials( string connectionString )
        {
            IEnumerable<string> connectionKeys = ParseConnectionString(connectionString).Keys;

            if ( connectionKeys.Any( a => a.Equals( "password", StringComparison.CurrentCultureIgnoreCase ) ) )
            {
                int indexOfPassword = connectionString.IndexOf("password", StringComparison.CurrentCultureIgnoreCase);
                return connectionString.Substring( 0, indexOfPassword );
            }
            else
            { return connectionString; }
        }

    }
}
