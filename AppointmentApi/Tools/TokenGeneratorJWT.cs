using AppointmentApi.Tools.Interfaces;
using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApi.Tools
{
    public class TokenGeneratorJWT : ITokenGenerator
    {
        public Token GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Login.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new Token
            {
                Expiration = (DateTime)tokenDescriptor.Expires,
                TokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor))
            };
        }
    }
}
