using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IEtatJouetService
    {
        public Task<ActionResult<List<EtatJouet>>> GetEtatJouets();
        public Task<ActionResult<EtatJouet>> GetEtatJouet(int id);
        public Task<bool> UpdateEtatJouet(EtatJouet etatJouet);
        public Task<bool> AddEtatJouet(EtatJouet etatJouet);
        public Task<bool> DeleteEtatJouet(int id);
    }
}
