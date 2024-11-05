using System.IdentityModel.Tokens.Jwt;
using TaskManager.DTOs.Users;

namespace TaskManager.Services.Tokens.Interface
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(UserDto userDto);
        string WriteToken(JwtSecurityToken token);
    }
}
