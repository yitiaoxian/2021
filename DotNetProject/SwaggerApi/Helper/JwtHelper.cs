using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.IdentityModel.Tokens;
using SwaggerApi.Models;

namespace SwaggerApi.Helper
{
 
    /// <summary>
    /// 
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(TokenModel tokenModel)
        {
            ///读取配置文件中的JWT设置信息
            string iss = AppConfigurtaionServices.Configuration["JwtSetting:Issuer"];
            string aud = AppConfigurtaionServices.Configuration["JwtSetting:Audience"];
            string secret = AppConfigurtaionServices.Configuration["JwtSetting:SecretKey"];

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                 //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud),
            };
            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);
            return encodedJwt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(jwtStr))
            {
                return null;
            }
            string iss = AppConfigurtaionServices.Configuration["JwtSetting:jwtIssuer"];
            JwtSecurityToken jwtSecurityToken = jwtHandler.ReadJwtToken(jwtStr);
            if (jwtSecurityToken.Issuer != iss)
            {
                return null;//不正确
            }
            if(jwtSecurityToken.ValidTo < DateTime.Now)
            {
                return null;//过期
            }
            object role;
            try
            {
                jwtSecurityToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch(Exception e)
            {
                LogHelper.Error(e.Message);
                throw;
            }
            var tm = new TokenModel
            {
                Uid = jwtSecurityToken.Id.ToString(),
                Role = role != null ? role.ToString() : ""
            };
            return tm;
        }
    }
}
