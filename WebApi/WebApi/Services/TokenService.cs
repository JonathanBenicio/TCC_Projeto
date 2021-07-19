using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Services
{
    public class TokenService
    {
        public static string GerenateToken(Login login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDes = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, login.Email.ToString()),

                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDes);
            return tokenHandler.WriteToken(token);

        }
    }
}
