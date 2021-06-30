using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SwaggerApi
{
    public static class JsonUtil
    {
        /// <summary>
        /// 将指定的json对象序列化成json数据
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            try
            {
                return null == obj ? null : JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将json数据反序列化成指定对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public static T ToObject<T> (this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch(Exception e)
            {
                LogHelper.Error(e.Message);
                return default(T);
            }
        }
    }
}
