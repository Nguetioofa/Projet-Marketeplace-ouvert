using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IModeLivraisonService
    {
        public Task<ActionResult<List<ModeLivraisonL>>> GetModeLivraisons();
        public Task<ActionResult<ModeLivraisonL>> GetModeLivraison(int id);
        public Task<bool> UpdateModeLivraison(ModeLivraisonL ModeLivraison);
        public Task<bool> AddModeLivraison(ModeLivraisonL ModeLivraison);
        public Task<bool> DeleteModeLivraison(int id);
    }
}
