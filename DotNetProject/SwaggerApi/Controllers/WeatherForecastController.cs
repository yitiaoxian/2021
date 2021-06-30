using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="services"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IEnumerable<ITestService> services)
        {
            _logger = logger;
            testServiceA = services.FirstOrDefault(x => x.GetType().Name == "TestServiceA");
            testServiceB = services.FirstOrDefault(x => x.GetType().Name == "TestServiceB");
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
            LogHelper.Info(testServiceA.GetHashCode().ToString());
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
            LogHelper.Info(testServiceB.GetHashCode().ToString());

            return new ComWebResponseEntity { Result = true,Content= testServiceA.MultiServicesTest(str) } ;
        }
    }
}
