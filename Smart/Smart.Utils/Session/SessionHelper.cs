
using System.Web;

namespace Smart.Utils
{
    /// <summary>
    /// Session 操作类
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// 根据session名获取session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns></returns>
        public static object GetSession(string strSessionName )
        {
            return HttpContext.Current.Session[ strSessionName ];
        }

        /// <summary>
        /// 添加默认有效期的session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="val">session 值</param>
        public static void AddSession(string strSessionName, object val)
        {
            HttpContext.Current.Session.Remove( strSessionName );
            HttpContext.Current.Session.Add( strSessionName, val);
        }

        /// <summary>
        /// 添加指定有效期的Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="objValue">Session值</param>
        /// <param name="iExpires">调动有效期（分钟）</param>
        public static void AddSession( string strSessionName, object objValue, int iExpires )
        {
            HttpContext.Current.Session[ strSessionName ] = objValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }


        /// <summary>
        /// 删除一个指定的session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns></returns>
        public static void RemoveSession(string strSessionName )
        {
            HttpContext.Current.Session.Remove( strSessionName );
        }

        /// <summary>
        /// 删除所有的session
        /// </summary>
        /// <param name="strSessionName">Session名称</param>
        /// <returns></returns>
        public static void RemoveAllSession(string strSessionName )
        {
            HttpContext.Current.Session.RemoveAll();
        }

    }
}
