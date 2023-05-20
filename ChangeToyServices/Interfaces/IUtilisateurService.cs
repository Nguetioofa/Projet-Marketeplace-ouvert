using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Users;
using System.Security.Claims;

namespace ChangeToyServices.Interfaces
{
    public interface IUtilisateurService
    {
         Task<ActionResult<List<UtilisateurL>>> GetUtilisateurs();
         Task<ActionResult<UtilisateurL>> GetUtilisateur(int id);
         Task<bool> UpdateUtilisateur(UtilisateurL Utilisateur);
         Task<bool> AddUtilisateur(UtilisateurL Utilisateur);
         Task<bool> DeleteUtilisateur(int id);
        Task<(UserTokensDto useraut, string errorMessage)> Login(UserAuthen model);
         Task<object> Register(UserResisterDto userResisterDto);

    }
}
