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

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UtilisateursController(EchangeJouetsContext context, IUserService userService, ITokenService tokenService)
        {
            _context = context;
            _userService = userService;
            _tokenService = tokenService;

        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
          if (_context.Utilisateurs == null)
          {
              return NotFound();
          }
            return await _context.Utilisateurs.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
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

            return utilisateur;
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

        [HttpGet("GetUserByIdJouet{id}")]
        public async Task<ActionResult<Utilisateur>> GetUserByIdJouet(int id)
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
            

            return Ok(User);
        }

        [HttpGet("GetUserByIdRole{id}")]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUserByIdRole(int id)
        {
            if (_context.Utilisateurs == null || _context.FonctionUsers == null || _context.Roles == null)
            {
                return NotFound();
            }


            var User = await _context.Utilisateurs.Where(u => !u.EstSupprimer)
                                            .Join(_context.FonctionUsers.Where(f => f.RolesId == id && !f.EstSupprimer),
                                                user => user.Id,
                                                fonc => fonc.IdUser,
                                                (user, fonc) => user)
                                                 .ToListAsync(); 

            if (User == null)
                return NotFound();

            return Ok(User);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthen model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email))
                return BadRequest(new
                {
                    message = "Email or password is incorrect"
                });
            var roles =  _context.Utilisateurs.Where(u => !u.EstSupprimer && u.Email.Equals(model.Email)).Join(
                                            _context.FonctionUsers.Where(f => !f.EstSupprimer),
                                            user => user.Id,
                                            fonction => fonction.RolesId,
                                            (user, fonction) => fonction).Join(
                                                _context.Roles.Where(r => !r.EstSupprimer),
                                                fonction => fonction.RolesId,
                                                role => role.Id,
                                                (fonction, role) => role).ToList();

            var token = _tokenService.GenerateToken(user, roles);
            // return basic user info (without password) and token to store client side
            return Ok(token);
        }


        private bool UtilisateurExists(int id)
        {
            return (_context.Utilisateurs.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
