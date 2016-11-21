using System;

namespace Smart.Core.Utils
{
    /// <summary>
    ///  异常信息帮助类
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// 向调用层抛出配置信息异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static ConfigException ThrowConfigException(string msg, Exception e = null)
        {
            string temp = msg;
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                temp = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                temp = "未知配置信息异常，详情请查看日志信息。";
            }
            return e == null
                ? new ConfigException(string.Format("配置信息异常：{0}", temp))
                : new ConfigException(string.Format("配置信息异常：{0}", temp), e);
        }

        /// <summary>
        /// 向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowDataAccessException(string msg, Exception e = null)
        {
            string temp = msg;
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                temp = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                temp = "未知数据访问层异常，详情请查看日志信息。";
            }

            return e == null
                ? new DataAccessException(string.Format("数据访问层异常：{0}", temp))
                : new DataAccessException(string.Format("数据访问层异常：{0}", temp), e);
        }

        /// <summary>
        ///     向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static BusinessException ThrowBusinessException( string msg, Exception e = null )
        {
            if ( string.IsNullOrEmpty( msg ) && e != null )
            {
                msg = e.Message;
            }
            else if ( string.IsNullOrEmpty( msg ) )
            {
                msg = "未知业务逻辑层异常，详情请查看日志信息。";
            }
            return e == null ? new BusinessException( string.Format( "业务逻辑层异常：{0}", msg ) ) : new BusinessException( string.Format( "业务逻辑层异常：{0}", msg ), e );
        }

    }
}
