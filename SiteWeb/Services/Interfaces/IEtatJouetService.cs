using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IEtatJouetService
    {
        public Task<List<EtatJouetL>> GetEtatJouets();
        public Task<EtatJouetL> GetEtatJouet(int id);
        public Task<bool> UpdateEtatJouet(EtatJouetL etatJouet);
        public Task<bool> AddEtatJouet(EtatJouetL etatJouet);
        public Task<bool> DeleteEtatJouet(int id);
    }
}
