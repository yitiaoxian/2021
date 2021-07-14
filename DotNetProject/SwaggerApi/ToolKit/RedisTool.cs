using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SwaggerApi.ToolKit
{
    /// <summary>
    /// redis工具类
    /// </summary>
    public class RedisTool
    {
        private static ConnectionMultiplexer redis;
        /// <summary>
        /// 获取redis连接
        /// </summary>
        /// <returns></returns>
        protected static ConnectionMultiplexer GetConnection()
        {
            if(redis == null || redis.IsConnected)
            {
                redis = ConnectionMultiplexer.Connect("localhost");
            }
            return redis;
        }
    }
}
