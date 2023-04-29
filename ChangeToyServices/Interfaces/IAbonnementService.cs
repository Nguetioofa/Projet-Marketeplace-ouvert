global using ModelsLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IAbonnementService
    {
        public Task<ActionResult<List<Abonnement>>> GetAbonnements();
        public Task<ActionResult<Abonnement>> GetAbonnement(int id);
        public Task<bool> UpdateAbonnement(Abonnement abonnement);
        public Task<bool> AddAbonnement(Abonnement abonnement);
        public Task<bool> DeleteAbonnement(int id);

    }
}
