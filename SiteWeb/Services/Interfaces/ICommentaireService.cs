using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface ICommentaireService
    {
        public Task<List<CommentaireL>> GetCommentaires();
        public Task<ActionResult<CommentaireL>> GetCommentaire(int id);
        public Task<bool> UpdateCommentaire(CommentaireL commentaire);
        public Task<CommentaireL> AddCommentaire(CommentaireL commentaire);
        public Task<bool> DeleteCommentaire(int id);
		public Task<List<CommentaireL>> GetCommentaireByIdJouet(int id);
		public Task<List<CommentaireL>> GetCommentaireByIdAnnonce(int id);


	}
}
