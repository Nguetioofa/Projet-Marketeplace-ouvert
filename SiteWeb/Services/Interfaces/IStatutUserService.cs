using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IStatutUserService
    {
        public Task<ActionResult<IEnumerable<StatutUserL>>> GetStatutUsers();
        public Task<ActionResult<StatutUserL>> GetStatutUser(int id);
        public Task<bool> UpdateStatutUser(StatutUserL StatutUser);
        public Task<bool> AddStatutUser(StatutUserL StatutUser);
        public Task<bool> DeleteStatutUser(int id);
    }
}
