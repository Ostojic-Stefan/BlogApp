using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var tokenSecret = _config["AuthOptions:TokenSecret"]!;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}