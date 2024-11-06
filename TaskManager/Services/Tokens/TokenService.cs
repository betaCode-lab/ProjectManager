using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.DTOs.Users;
using TaskManager.Services.Tokens.Interface;

namespace TaskManager.Services.Tokens
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _configurationManager;

        public TokenService(IConfiguration configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public JwtSecurityToken GenerateAccessToken(UserDto userDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userDto.Firstname),
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configurationManager["Token:Issuer"],
                audience: _configurationManager["Token:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationManager["Token:SecretKey"]!)), SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public string WriteToken(JwtSecurityToken token)
        {
            if (token == null)
            {
                return string.Empty;
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
