using ApiWeb.ModelDto;
using ApiWeb.Models;

namespace ApiWeb.Services
{
    public interface ITokenService
    {
        UserTokens GenerateToken(UserDto user, List<Role> roles);
        int? ValidateToken(string token);

    }
}
