using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using SwaggerApi.Models;

namespace SwaggerApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ITestService testServiceA;
        private readonly ITestService testServiceB;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRedisCacheClient redisCacheClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="services"></param>
        /// <param name="_redisCacheClient"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IEnumerable<ITestService> services,IRedisCacheClient _redisCacheClient)
        {
            _logger = logger;
            testServiceA = services.FirstOrDefault(x => x.GetType().Name == "TestServiceA");
            testServiceB = services.FirstOrDefault(x => x.GetType().Name == "TestServiceB");
            redisCacheClient = _redisCacheClient;
        }
        /// <summary>
        /// Get测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /// <summary>
        /// TestServiceA
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TestServiceA")]
        public async Task<HttpResponseMessage> TestServiceA(string str)
        {
            ComWebResponseEntity result = new ComWebResponseEntity();
            //LogHelper.Info(testServiceA.GetHashCode().ToString());
            List<string> re = new List<string> { "ssd", "123" };
            Dictionary<int, List<string>> ds = new Dictionary<int, List<string>>();
            ds.Add(1, re);
            result.Result = true;
            result.Content = ds;
            return new HttpResponseMessage { Content = new StringContent(result.ToJson(), Encoding.UTF8, "application/json") };
        }
        /// <summary>
        /// TestServiceB
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TestServiceB")]
        public async Task<ComWebResponseEntity> TestServiceB(string str)
        {
            //LogHelper.Info(testServiceB.GetHashCode().ToString());

            return new ComWebResponseEntity { Result = true,Content= testServiceA.MultiServicesTest(str) } ;
        }
        /// <summary>
        /// TestJsonSeriliza
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TestJsonSeriliza")]
        public async Task<ComWebResponseEntity> TestJsonSeriliza(string str)
        {
            List<double> a = new List<double> {1.23,123.2,1.2 };
            string b = a.ToJson();
            List<double> a1 = new();
            a1 = b.ToObject<List<double>>();
            a1.RemoveAt(2);
            List<double> a2 = a;
            return new ComWebResponseEntity { Result = true, Content = testServiceA.MultiServicesTest(str) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AddProductTest")]
        public async Task<string> AddProductTest()
        {
            var product = new Product { Id=1,Name= "hand sanitizer", Price=100};
            bool isAdded = await redisCacheClient.Db0.AddAsync("Product", product, DateTimeOffset.Now.AddMinutes(10));
            //bool isAdded2 = await redisCacheClient.Db0.AddAsync("Product", product.Id=2, DateTimeOffset.Now.AddMinutes(10));
            return isAdded?"true":"false";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductTest")]
        public async Task<string> GetProductTest()
        {
            var product = new Product { Id = 1, Name = "hand sanitizer", Price = 100 };
            Product isAdded = await redisCacheClient.Db0.GetAsync<Product>("Product",CommandFlags.None);
            //bool isAdded2 = await redisCacheClient.Db0.AddAsync("Product", product.Id=2, DateTimeOffset.Now.AddMinutes(10));
            return isAdded.ToJson();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DelProductTest")]
        public async Task<string> DelProductTest()
        {
            var product = new Product { Id = 1, Name = "hand sanitizer", Price = 100 };
            bool isAdded = await redisCacheClient.Db0.RemoveAsync("Product",CommandFlags.None);
            //bool isAdded2 = await redisCacheClient.Db0.AddAsync("Product", product.Id=2, DateTimeOffset.Now.AddMinutes(10));
            return isAdded ? "true" : "false";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SetProductTest")]
        public async Task<string> SetProductTest()
        {
            var product = new Product { Id = 1, Name = "hand sanitizer", Price = 100 };
            List<string> ite = new List<string>() { "sd","sdawe"};
            bool isAdded = await redisCacheClient.Db0.SetAddAsync("set",ite);
            //bool isAdded2 = await redisCacheClient.Db0.AddAsync("Product", product.Id=2, DateTimeOffset.Now.AddMinutes(10));
            return isAdded ? "true" : "false";
        }
    }
}
