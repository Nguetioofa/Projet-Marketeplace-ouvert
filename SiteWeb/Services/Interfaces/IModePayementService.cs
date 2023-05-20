using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IModePayementService
    {
        public Task<ActionResult<List<ModePayement>>> GetModePayements();
        public Task<ActionResult<ModePayement>> GetModePayement(int id);
        public Task<bool> UpdateModePayement(ModePayement ModePayement);
        public Task<bool> AddModePayement(ModePayement ModePayement);
        public Task<bool> DeleteModePayement(int id);
    }
}
