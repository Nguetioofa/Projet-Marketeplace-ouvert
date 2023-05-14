using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Users;

namespace ChangeToyServices.Interfaces
{
    public interface IUtilisateurService
    {
         Task<ActionResult<List<UtilisateurL>>> GetUtilisateurs();
         Task<ActionResult<UtilisateurL>> GetUtilisateur(int id);
         Task<bool> UpdateUtilisateur(UtilisateurL Utilisateur);
         Task<bool> AddUtilisateur(UtilisateurL Utilisateur);
         Task<bool> DeleteUtilisateur(int id);
         Task<ActionResult<object>> Login(UserAuthen model);
         Task<object> Register(UserResisterDto userResisterDto);

    }
}
