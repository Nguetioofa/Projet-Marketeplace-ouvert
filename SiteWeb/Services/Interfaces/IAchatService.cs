using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Achats;

namespace SiteWeb.Services.Interfaces
{
    public interface IAchatService
    {
        public Task<ActionResult<List<AchatL>>> GetAchats();
        public Task<ActionResult<AchatL>> GetAchat(int id);
        public Task<bool> UpdateAchat(AchatL Achat);
        public Task<bool> AddAchat(AchatL Achat);
        public Task<bool> DeleteAchat(int id);

    }
}
