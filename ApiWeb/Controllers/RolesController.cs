using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Models;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public RolesController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return await _context.Roles.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            var role = await _context.Roles.Where(c => !c.EstSupprimer)
                                                .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutRole(Role role)
        {
            //if (id != role.Id)
            //{
            //    return BadRequest();
            //}
            if (!RoleExists(role.Id))
            {
                return NotFound();
            }
            _context.Entry(role).State = EntityState.Modified;

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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
          if (_context.Roles == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Roles'  is null.");
          }
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var role = await _context.Roles.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (role == null)
            {
                return NotFound();
            }

            role.EstSupprimer = true;
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetRolesByEmailUser{Email}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesByEmailUser(string email)
        {
            if (_context.Jouets == null)
            {
                return NotFound();
            }

            var roles = await _context.Utilisateurs.Where(u => !u.EstSupprimer && u.Email.Equals(email))
                                        .Join( _context.FonctionUsers.Where(f => !f.EstSupprimer),
                                            user => user.Id,
                                            fonction => fonction.RolesId,
                                            (user, fonction) => fonction)
                                            .Join( _context.Roles.Where(r => !r.EstSupprimer),
                                             fonction => fonction.RolesId,
                                             role => role.Id,
                                             (fonction, role) => role).ToListAsync();

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }

 /*       [HttpGet("GetRoleByIdUser{id}")]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetRoleByIdUser(int id)
        {
            if (_context.Utilisateurs == null || _context.FonctionUsers == null || _context.Roles == null)
            {
                return NotFound();
            }


            var User = await _context.Utilisateurs.Where(u => !u.EstSupprimer)
                                                                .Where(u => u.navig == id)
                                                                .Select(u => u.FonctionUsers)
                                                                .Join(_context.Roles.Where(r=>!r.EstSupprimer),
                                                                fonction => fonction)
                                                 .ToListAsync();

            if (User == null)
                return NotFound();

            return Ok(User);
        }*/

        private bool RoleExists(int id)
        {
            return (_context.Roles.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
