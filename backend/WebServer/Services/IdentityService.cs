using LangLearner.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LangLearner.Services
{
    public interface IIdentityService
    {
        public string GenerateToken(TokenClaims request);
    }

    public class IdentityService : IIdentityService
    {
        private readonly string TokenSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "secret4w78efhc2783gd671872e2@!WDX!@#!~!@$!@E@!1wd12";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromDays(100);

        public string GenerateToken(TokenClaims request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, request.Email),
                new("UserId", request.UserId.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "https://LangLearner.com",
                Audience = "https://LangLearner.com",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
