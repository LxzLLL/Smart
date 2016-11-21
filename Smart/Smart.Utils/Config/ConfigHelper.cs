using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Smart.Core.Utils
{
	/// <summary>
	/// web.config������
	/// </summary>
	public sealed class ConfigHelper
	{
        /// <summary>
        /// �õ�AppSettings�е������ַ�����Ϣ�������뻺���У���Ϊ"AppSettings_" + key��
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
		/// �õ�AppSettings�е�����Bool��Ϣ
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
		/// �õ�AppSettings�е�����Decimal��Ϣ
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
		/// �õ�AppSettings�е�����int��Ϣ
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
        /// �����ַ���
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
                throw ExceptionHelper.ThrowConfigException( "��������Ϊ��" + key + "�������ݿ�������Ϣ������web.config�ļ��е����á�" );
            }
        }

        /// <summary>
        /// �ṩ��������
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
                throw ExceptionHelper.ThrowConfigException( "��������Ϊ��" + key + "�������ݿ�������Ϣ������web.config�ļ��е����á�" );
            }
        }

        /// <summary>
        /// ���Ի�ȡָ�����Ƶ����ݿ�������Ϣ������������Ϣ�����򷵻� true��������������ķ�ʽ�������ݿ������ַ��������ݿ�ܵ����ƣ�����������Ϣ�������򷵻� false��
        /// </summary>
        /// <param name="key">ָ�������ݿ���������</param>
        /// <param name="connectionString">�����������ʾ��ȡ�������ݿ������ַ���</param>
        /// <param name="providerName">�����������ʾ��ȡ�������ݿ�ܵ�����</param>
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
        /// �����ļ�����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AppSetting( string key )
        {
            var set = ConfigurationManager.AppSettings[key];

            if ( set != null ) { return set; }
            throw ExceptionHelper.ThrowConfigException( "��������Ϊ��" + key + "����AppSetting������Ϣ��������Ӧ�����ļ��е����á�" );
        }

        /// <summary>
        /// ���Ի�ȡָ�����Ƶ�AppSetting������Ϣ��������Ϣ�����򷵻� true��������������ķ�ʽ������ֵ��������Ϣ�������򷵻� false��
        /// </summary>
        /// <param name="key">ָ������������</param>
        /// <param name="value">�����������ʾ��ȡ����ֵ</param>
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
        /// ����ָ�������ݿ������ַ��������Լ�ֵ�Է�ʽ�������е� ������������ֵ
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <returns>һ����ֵ�Լ��ϣ����� key Ϊ�������� value Ϊ����ֵ��</returns>
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
        /// ����ָ�������ݿ������ַ��������ز�����ȫƾ����Ϣ�����ݿ������ַ���ֵ
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <returns>������ȫƾ����Ϣ�����ݿ������ַ���ֵ</returns>
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
