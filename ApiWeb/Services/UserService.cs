﻿using ApiWeb.ModelDto;
using ApiWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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
            byte[] passwordHash;
            byte[] passwordSalt;

            SelectPassWordAndSalt(email, out passwordHash, out passwordSalt);

 
            if (!VerifyPasswordHash(password,passwordHash,passwordSalt)) 
            {
                return new UserDto();
            }

            var user = _context.Utilisateurs.Where(u => !u.EstSupprimer)
                                                     .Where(u => u.Email == email).FirstOrDefault();

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Nom,
                Password = password
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


        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public void SelectPassWordAndSalt(string email, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var user = _context.Utilisateurs.Where(u => !u.EstSupprimer)
                                 .Where(u => u.Email == email)
                                 .Select(u => u).FirstOrDefault();


                passwordSalt = user?.Sel;
                passwordHash = user?.MotDePasse;


        }

        public List<Role> RolesByEmail(string email)
        {
            if (_context.Utilisateurs is null || _context.FonctionUsers is null || _context.Roles is null)
                return null;

            var role =  _context.Utilisateurs.Where(u => !u.EstSupprimer && u.Email.Equals(email)).Join(
                                            _context.FonctionUsers.Where(f => !f.EstSupprimer),
                                            user => user.Id,
                                            fonction => fonction.IdUser,
                                            (user, fonction) => fonction).Join(
                                                _context.Roles.Where(r => !r.EstSupprimer),
                                                fonction => fonction.RolesId,
                                                role => role.Id,
                                                (fonction, role) => role).ToList();

            return role;

        }

        public string NameLastNameByEmail(string email)
        {
            if (_context.Utilisateurs is null)
                return null;

            var user = _context.Utilisateurs.Where(u => !u.EstSupprimer && u.Email.Equals(email))
                                            .FirstOrDefault();

            return user.Nom + " " + user.Prenom;
        }
    }
}
