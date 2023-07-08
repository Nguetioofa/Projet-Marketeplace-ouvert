using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IFonctionUserService
    {
        public Task<ActionResult<List<FonctionUserL>>> GetFonctionUsers();
        public Task<ActionResult<FonctionUserL>> GetFonctionUser(int id);
        public Task<bool> UpdateFonctionUser(FonctionUserL FonctionUser);
        public Task<bool> AddFonctionUser(FonctionUserL FonctionUser);
        public Task<bool> DeleteFonctionUser(int id);
    }
}
