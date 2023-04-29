using ApiWeb.ModelDto;
using ApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.Services
{
    
    public class UserService : IUserService
    {
        EchangeJouetsContext _context;

        public UserService(EchangeJouetsContext context)
        {
            _context = context;
        }

        public UserDto Authenticate(string email, string password)
        {
            var user = _context.Utilisateurs.Where(u => !u.EstSupprimer)
                                                       .Where(u => u.Email == email)
                                                       .Where(u => u.MotDePasse == password).FirstOrDefault();
            if (user == null) 
            {
                return new UserDto();
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Nom,
                Password = user.MotDePasse
            };

            return userDto;
        }

        public Utilisateur GetById(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return null;
            }
            var utilisateur =  _context.Utilisateurs.Where(c => !c.EstSupprimer)
                                                             .Where(ca => ca.Id == id).FirstOrDefault();
            return utilisateur;
        }
    }
}
