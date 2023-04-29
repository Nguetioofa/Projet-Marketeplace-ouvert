using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IJouetService
    {
        public Task<ActionResult<List<Jouet>>> GetJouets();
        public Task<ActionResult<Jouet>> GetJouet(int id);
        public Task<bool> UpdateJouet(Jouet Jouet);
        public Task<bool> AddJouet(Jouet Jouet);
        public Task<bool> DeleteJouet(int id);
    }
}
