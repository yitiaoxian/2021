using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SwaggerApi;

namespace MqttClientConnection
{
    public class HttpClientHelper
    {
        public static string GetHttpResponse(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception($"调用接口出错，StatusCode：{response.StatusCode}");
            }
        }

        public static async Task<string> GetHttpResponseAsync(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"调用接口出错，StatusCode：{response.StatusCode}");
            }
        }

        public static string PostHttpResponse(string url, string content)
        {
            HttpContent httpContent = new StringContent(content);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception($"调用接口出错，StatusCode：{response.StatusCode}");
            }
        }

        public static async Task<string> PostHttpResponseAsync(string url, string content)
        {
            HttpContent httpContent = new StringContent(content);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"调用接口出错，StatusCode：{response.StatusCode}");
            }
        }

        /// <summary>
        /// 用 GET 方式调用接口，并将返回的 JSON 格式的结果转换成指定类型的对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ComWebResponse GetWebResponse(string url)
        {
            try
            {
                string requestString = GetHttpResponse(url);
                return requestString.ToObject<ComWebResponse>();
            }
            catch (Exception ex)
            {
                return new ComWebResponse { Result = false, Error = ex.Message };
            }
        }

        /// <summary>
        /// 用 POST 方式调用接口，并将返回的 JSON 格式的结果转换成指定类型的对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static ComWebResponse GetWebResponse(string url, string parameter)
        {
            try
            {
                string requestString = PostHttpResponse(url, parameter);
                return requestString.ToObject<ComWebResponse>();
            }
            catch (Exception ex)
            {
                return new ComWebResponse { Result = false, Error = ex.Message };
            }
        }

        /// <summary>
        /// 用 GET 方式调用接口，并将返回的 JSON 格式的结果转换成指定类型的对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<ComWebResponse> GetWebResponseAsync(string url)
        {
            try
            {
                string requestString = await GetHttpResponseAsync(url);
                return requestString.ToObject<ComWebResponse>();
            }
            catch (Exception ex)
            {
                return new ComWebResponse { Result = false, Error = ex.Message };
            }
        }

        /// <summary>
        /// 用 POST 方式调用接口，并将返回的 JSON 格式的结果转换成指定类型的对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static async Task<ComWebResponse> GetWebResponseAsync(string url, string parameter)
        {
            try
            {
                string requestString = await PostHttpResponseAsync(url, parameter);
                return requestString.ToObject<ComWebResponse>();
            }
            catch (Exception ex)
            {
                return new ComWebResponse { Result = false, Error = ex.Message };
            }
        }
    }
}
