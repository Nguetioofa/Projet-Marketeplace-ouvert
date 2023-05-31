using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IEtatJouetService
    {
        public Task<List<EtatJouet>> GetEtatJouets();
        public Task<EtatJouet> GetEtatJouet(int id);
        public Task<bool> UpdateEtatJouet(EtatJouet etatJouet);
        public Task<bool> AddEtatJouet(EtatJouet etatJouet);
        public Task<bool> DeleteEtatJouet(int id);
    }
}
