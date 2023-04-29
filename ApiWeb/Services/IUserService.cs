using ApiWeb.ModelDto;
using ApiWeb.Models;

namespace ApiWeb.Services
{
    public interface IUserService
    {
        Utilisateur GetById(int  id);
        UserDto Authenticate(string email, string password);
    }
}
