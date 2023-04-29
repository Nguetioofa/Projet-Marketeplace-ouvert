using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IAchatService
    {
        public Task<ActionResult<List<Achat>>> GetAchats();
        public Task<ActionResult<Achat>> GetAchat(int id);
        public Task<bool> UpdateAchat(Achat Achat);
        public Task<bool> AddAchat(Achat Achat);
        public Task<bool> DeleteAchat(int id);

    }
}
