using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface ICategorieJouetService
    {
        public Task<ActionResult<List<CategorieJouet>>> GetCategorieJouets();
        public Task<ActionResult<CategorieJouet>> GetCategorieJouet(int id);
        public Task<bool> UpdateCategorieJouet(CategorieJouet value);
        public Task<bool> AddCategorieJouet(CategorieJouet value);
        public Task<bool> DeleteCategorieJouet(int id);

    }
}
