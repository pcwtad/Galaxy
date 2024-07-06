using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Infrastructure
{
    public class JWT
    {
        public string GetJwt(string usermail)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("usermail", usermail));
            //claims.Add(new Claim(ClaimTypes.Role, "admin"));//颁发管理员角色
            string key = "awfasgjiowqpuurio12352opoijaops12";
            DateTime expire = DateTime.Now.AddDays(7);
            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var seckey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(seckey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }
    }
}
