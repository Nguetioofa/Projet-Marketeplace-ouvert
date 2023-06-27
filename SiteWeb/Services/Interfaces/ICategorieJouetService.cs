using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface ICategorieJouetService
    {
        public Task<List<CategorieJouet>> GetCategorieJouets();
        public Task<CategorieJouet> GetCategorieJouet(int id);
        public Task<bool> UpdateCategorieJouet(CategorieJouet value);
        public Task<bool> AddCategorieJouet(CategorieJouet value);
        public Task<bool> DeleteCategorieJouet(int id);

    }
}
