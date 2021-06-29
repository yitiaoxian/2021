using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerApi.Helper;
using SwaggerApi.Models;

namespace SwaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
    }
}
