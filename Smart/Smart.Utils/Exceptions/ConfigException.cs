using System;
using System.Runtime.Serialization;

namespace Smart.Core.Utils
{
    /// <summary>
    /// 配置信息异常类，用于封装由错误的配置信息引发的异常，以供 业务应用层 抓取
    /// </summary>
    [Serializable]
    public class ConfigException : Exception
    {
        /// <summary>
        ///     实例化一个 ConfigException 类的新实例
        /// </summary>
        public ConfigException() { }

        /// <summary>
        ///     使用异常消息实例化一个 ConfigException 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public ConfigException(string message)
            : base(message) { }

        /// <summary>
        ///     使用异常消息与一个内部异常实例化一个 ConfigException 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在ConfigException内部的异常实例</param>
        public ConfigException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        ///     使用可序列化数据实例化一个 ConfigException 类的新实例
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected ConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
