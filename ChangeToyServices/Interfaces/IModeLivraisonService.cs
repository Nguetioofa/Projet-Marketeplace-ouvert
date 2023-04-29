using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IModeLivraisonService
    {
        public Task<ActionResult<List<ModeLivraison>>> GetModeLivraisons();
        public Task<ActionResult<ModeLivraison>> GetModeLivraison(int id);
        public Task<bool> UpdateModeLivraison(ModeLivraison ModeLivraison);
        public Task<bool> AddModeLivraison(ModeLivraison ModeLivraison);
        public Task<bool> DeleteModeLivraison(int id);
    }
}
