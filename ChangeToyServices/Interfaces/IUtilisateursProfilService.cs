using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IUtilisateursProfilService
    {
        public Task<ActionResult<List<UtilisateursProfil>>> GetUtilisateursProfils();
        public Task<ActionResult<UtilisateursProfil>> GetUtilisateursProfil(int id);
        public Task<bool> UpdateUtilisateursProfil(UtilisateursProfil UtilisateursProfil);
        public Task<bool> AddUtilisateursProfil(UtilisateursProfil UtilisateursProfil);
        public Task<bool> DeleteUtilisateursProfil(int id);
    }
}
