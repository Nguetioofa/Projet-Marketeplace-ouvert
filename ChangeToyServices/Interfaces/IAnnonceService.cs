using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IAnnonceService
    {
        public Task<ActionResult<List<Annonce>>> GetAnnonces();
        public Task<ActionResult<Annonce>> GetAnnonce(int id);
        public Task<bool> UpdateAnnonce(Annonce annonce);
        public Task<bool> AddAnnonce(Annonce annonce);
        public Task<bool> DeleteAnnonce(int id);

    }
}
