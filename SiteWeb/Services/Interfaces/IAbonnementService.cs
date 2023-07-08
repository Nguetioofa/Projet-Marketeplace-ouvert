global using ModelsLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IAbonnementService
    {
        public Task<ActionResult<List<AbonnementL>>> GetAbonnements();
        public Task<ActionResult<AbonnementL>> GetAbonnement(int id);
        public Task<bool> UpdateAbonnement(AbonnementL abonnement);
        public Task<bool> AddAbonnement(AbonnementL abonnement);
        public Task<bool> DeleteAbonnement(int id);

    }
}
