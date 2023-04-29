using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IFonctionUserService
    {
        public Task<ActionResult<List<FonctionUser>>> GetFonctionUsers();
        public Task<ActionResult<FonctionUser>> GetFonctionUser(int id);
        public Task<bool> UpdateFonctionUser(FonctionUser FonctionUser);
        public Task<bool> AddFonctionUser(FonctionUser FonctionUser);
        public Task<bool> DeleteFonctionUser(int id);
    }
}
