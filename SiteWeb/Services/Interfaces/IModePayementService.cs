using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IModePayementService
    {
        public Task<ActionResult<List<ModePayementL>>> GetModePayements();
        public Task<ActionResult<ModePayementL>> GetModePayement(int id);
        public Task<bool> UpdateModePayement(ModePayementL ModePayement);
        public Task<bool> AddModePayement(ModePayementL ModePayement);
        public Task<bool> DeleteModePayement(int id);
    }
}
