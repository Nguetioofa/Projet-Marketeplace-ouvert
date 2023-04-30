using ApiWeb.ModelDto;
using ApiWeb.Models;

namespace ApiWeb.Services
{
    public interface IUserService
    {
        Utilisateur GetById(int  id);
        UserDto Authenticate(string email, string password);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        void SelectPassWordAndSalt(string email, out byte[] passwordHash, out byte[] passwordSalt);

    }
}
