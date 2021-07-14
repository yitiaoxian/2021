using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MqttClientConnection
{
    class WebReply
    {
    }
    /// <summary>
    /// web api 返回的数据模型
    /// </summary>
    public class ComWebResponseEntity
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty(PropertyName = "Content", NullValueHandling = NullValueHandling.Ignore)]
        public object Content { get; set; }
    }
    public class ComWebResponseEntity<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty(PropertyName = "Content", NullValueHandling = NullValueHandling.Ignore)]
        public T Content { get; set; }
    }
    public class ComWebResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty(PropertyName = "Content", NullValueHandling = NullValueHandling.Ignore)]
        public object Content { get; set; }
    }
}
