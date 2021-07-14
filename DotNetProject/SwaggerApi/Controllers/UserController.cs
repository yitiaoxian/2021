using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerApi.Helper;
using SwaggerApi.Models;

namespace SwaggerApi.Controllers
{
    /// <summary>
    /// user
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITestService testServiceA;
        private readonly ITestService testServiceB;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="services"></param>
        public UserController(IEnumerable<ITestService> services)
        {
            testServiceA = services.FirstOrDefault(x => x.GetType().Name == "TestServiceA");
            testServiceB = services.FirstOrDefault(x => x.GetType().Name == "TestServiceB");

        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="role">用户</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string role)
        {
            string jwtStr = String.Empty;
            bool succ = false;
            if (role != null)
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中
                TokenModel tokenModel = new TokenModel { Uid = "abcd", Role = role};
                jwtStr = JwtHelper.IssueJwt(tokenModel);//登录，获取到一定规则的Token令牌
                succ = true;
            }
            else
            {
                jwtStr = "Login fail!!!";
            }
            return Ok(new { 
                success = succ,
                token  = jwtStr
            });
        }
        /// <summary>
        /// TestServiceA
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TestServiceA")]
        public IActionResult TestServiceA(string str)
        {
            //LogHelper.Info(testServiceA.GetHashCode().ToString());
            bool succ = true;
            return Ok(new
            {
                success = succ,
                result = testServiceA.GetHashCode().ToString()
            });
        }
        /// <summary>
        /// TestServiceB
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpGet]        
        [Route("TestServiceB")]
        public async Task<IActionResult> TestServiceB(string str)
        {
            //LogHelper.Info(testServiceB.GetHashCode().ToString());
            string a = String.Empty;
            await Task.Run(()=> {
                //Thread.Sleep(10000);
                a = testServiceB.MultiServicesTest(str);
            });
            bool succ = true;

            return Ok(new
            {
                success = succ,
                result = a
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TestJsonInput")]
        public async Task<IActionResult> TestJsonInput(User user)
        {
            //LogHelper.Info(testServiceB.GetHashCode().ToString());
            string a = String.Empty;
            await Task.Run(() => {
                //Thread.Sleep(10000);
                a = testServiceB.MultiServicesTest(user.Name);
            });
            bool succ = true;

            return Ok(new
            {
                success = succ,
                result = a
            });
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TestQueue")]
        [NonAction]
        public async Task<IActionResult> TestQueue(int i)
        {
            Queue<User> users = new Queue<User>();
            User u = new User();
            await Task.Run(()=> {
                for (int j = 0; j < 10; j++)
                {
                    users.Enqueue(new User
                    {
                        Gender = $"gender+{j}",
                        Name = $"name+{j}"
                    });
                }
                u = users.Peek();
            });
            return Ok(u);         
        }
    }
   /// <summary>
   /// 
   /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
       public string Gender { set; get; }
        /// <summary>
        /// 
        /// </summary>
       public string Name { set; get; }
    }
}
