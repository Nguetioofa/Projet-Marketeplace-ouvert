using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IUtilisateurService
    {
        public Task<ActionResult<List<Utilisateur>>> GetUtilisateurs();
        public Task<ActionResult<Utilisateur>> GetUtilisateur(int id);
        public Task<bool> UpdateUtilisateur(Utilisateur Utilisateur);
        public Task<bool> AddUtilisateur(Utilisateur Utilisateur);
        public Task<bool> DeleteUtilisateur(int id);
    }
}
