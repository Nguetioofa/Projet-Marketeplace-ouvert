using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface ICommentaireService
    {
        public Task<ActionResult<List<Commentaire>>> GetCommentaires();
        public Task<ActionResult<Commentaire>> GetCommentaire(int id);
        public Task<bool> UpdateCommentaire(Commentaire commentaire);
        public Task<bool> AddCommentaire(Commentaire commentaire);
        public Task<bool> DeleteCommentaire(int id);

    }
}
