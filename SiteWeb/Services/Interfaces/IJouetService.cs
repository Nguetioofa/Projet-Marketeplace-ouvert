using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IJouetService
    {
        public Task<List<Jouet>> GetJouets();
        public Task<Jouet> GetJouet(int id);
        public Task<bool> UpdateJouet(Jouet Jouet);
        public Task<Jouet> AddJouet(Jouet Jouet);
        public Task<bool> DeleteJouet(int id);
    }
}
