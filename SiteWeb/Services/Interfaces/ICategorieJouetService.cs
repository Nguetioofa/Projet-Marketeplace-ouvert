using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface ICategorieJouetService
    {
        public Task<List<CategorieJouetL>> GetCategorieJouets();
        public Task<CategorieJouetL> GetCategorieJouet(int id);
        public Task<bool> UpdateCategorieJouet(CategorieJouetL value);
        public Task<bool> AddCategorieJouet(CategorieJouetL value);
        public Task<bool> DeleteCategorieJouet(int id);

    }
}
