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
                Subject = new ClaimsIdentity(GetClaims(user)),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new Token
            {
                Expiration = (DateTime)tokenDescriptor.Expires,
                TokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                Roles = user.Roles,
                UserId = user.UserId
            };
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            return user.Roles.Select(r => new Claim(ClaimTypes.Role, r))
                .Append(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()))
                .Append(new Claim(ClaimTypes.Name, user.Login.ToString()));
        }
    }
}
