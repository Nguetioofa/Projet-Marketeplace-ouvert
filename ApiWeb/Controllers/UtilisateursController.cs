using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Models;
using ApiWeb.Services;
using Microsoft.AspNetCore.Authorization;
using ApiWeb.ModelDto;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using NuGet.Protocol;
using Microsoft.AspNetCore.Http.HttpResults;
using ModelsLibrary.Models.Users;
using ModelsLibrary.Models;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        public UtilisateursController(EchangeJouetsContext context, IUserService userService, ITokenService tokenService, IEmailSender emailSender)
        {
            _context = context;
            _userService = userService;
            _tokenService = tokenService;
            _emailSender = emailSender;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetUtilisateurs()
        {
          if (_context.Utilisateurs == null)
          {
              return NotFound();
          }
            var users = await _context.Utilisateurs.Where(ab => !ab.EstSupprimer).ToListAsync();
            var utilisateurs = new List<UtilisateurDto>() { };

            users.ForEach(user => utilisateurs.Add(new UtilisateurDto(user)));
                
            return Ok( utilisateurs );
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurDto>> GetUtilisateur(int id)
        {
          if (_context.Utilisateurs == null)
          {
              return NotFound();
          }
            var utilisateur = await _context.Utilisateurs.Where(c => !c.EstSupprimer)
                                                             .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (utilisateur == null)
            {
                return NotFound();
            }

            return Ok(new UtilisateurDto(utilisateur));
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUtilisateur(Utilisateur utilisateur)
        {
            //if (id != utilisateur.Id)
            //{
            //    return BadRequest();
            //}

            if (!UtilisateurExists(utilisateur.Id))
            {
                return NotFound();
            }
            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return NoContent();
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
          if (_context.Utilisateurs == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Utilisateurs'  is null.");
          }
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.Id }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return NotFound();
            }
            var utilisateur = await _context.Utilisateurs.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (utilisateur == null)
            {
                return NotFound();
            }

            utilisateur.EstSupprimer = true;
            _context.Entry(utilisateur).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetUserByIdJouet/{id}")]
        public async Task<ActionResult<UtilisateurDto>> GetUserByIdJouet(int id)
        {
            if (_context.Utilisateurs == null || _context.Jouets == null)
            {
                return NotFound();
            }

            var User = await _context.Utilisateurs.Where(u => !u.EstSupprimer)
                                                     .Join(_context.Jouets.Where(J => !J.EstSupprimer && J.Id == id),
                                                     user => user.Id,
                                                     jouet => jouet.Proprietaire,
                                                     (user , jouet) => user
                                                     ).FirstOrDefaultAsync();


            if (User == null)
                return NotFound();
            

            return Ok( new UtilisateurDto(User));
        }

        [HttpGet("GetUserByIdRole{id}")]
        public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetUserByIdRole(int id)
        {
            if (_context.Utilisateurs == null || _context.FonctionUsers == null || _context.Roles == null)
            {
                return NotFound();
            }


            var users = await _context.Utilisateurs.Where(u => !u.EstSupprimer)
                                            .Join(_context.FonctionUsers.Where(f => f.RolesId == id && !f.EstSupprimer),
                                                user => user.Id,
                                                fonc => fonc.IdUser,
                                                (user, fonc) => user)
                                                 .ToListAsync(); 

            if (users == null)
                return NotFound();
            var utilisateurs = new List<UtilisateurDto>() { };

            users.ForEach(user => utilisateurs.Add(new UtilisateurDto(user)));

            return Ok(utilisateurs);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserTokens>> Login(UserAuthen model)
        {
            if (!UtilisateurExists(model.Email))
                return BadRequest("Cette email n'existe pas");
           
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user is null || string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Email or password is incorrect" );

            var roles = _userService.RolesByEmail(model.Email);// RolesByEmail(user.Email).Result;

            if (roles is null)
                return BadRequest("Un probleme est survenu");

            var token = _tokenService.GenerateToken(user, roles);
            //await _emailSender.SendEmailAsync("nguetioof@gmail.com", "Test mail", $"Voici ton token {token.Token}");

            return Ok(token);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserResisterDto userResisterDto)
        {

            if (_context.Utilisateurs == null)
            {
                return BadRequest("Entity set 'EchangeJouetsContext.Utilisateurs'  is null." );
            }

            
            if (UtilisateurExists(userResisterDto.Email))
            {
                return BadRequest("Cet email existe deja");
            }
            if (NumberExists(userResisterDto.Telephone))
            {
                return BadRequest("Ce numero de telephone existe deja");
            }
            _userService.CreatePasswordHash(userResisterDto.MotDePasse, out byte[] passwordHash, out byte[] passwordSalt);

            var userregister = new Utilisateur()
            {
                Nom = userResisterDto.Nom,
                Prenom = userResisterDto.Prenom,
                Email = userResisterDto.Email,
                MotDePasse = passwordHash,
                Sel = passwordSalt,
                Telephone = userResisterDto.Telephone,
                Adresse = userResisterDto.Adresse,
                VilleUser = userResisterDto.VilleUser,
                QuatierUser = userResisterDto.QuatierUser
            }; 

            _context.Utilisateurs.Add(userregister);
            await _context.SaveChangesAsync();

            return Ok("enregistrement terminer");

        }

        private bool UtilisateurExists(int id)
        {
            return (_context.Utilisateurs.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool UtilisateurExists(string email)
        {
            return (_context.Utilisateurs.Any(e => e.Email == email));
        }

        private bool NumberExists(string numero)
        {
            return (_context.Utilisateurs.Any(e => e.Telephone == numero));
        }
    }
}
