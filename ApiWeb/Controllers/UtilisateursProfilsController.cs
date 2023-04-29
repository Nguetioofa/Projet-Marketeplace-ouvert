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
    public class UtilisateursProfilsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public UtilisateursProfilsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/UtilisateursProfils
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateursProfil>>> GetUtilisateursProfils()
        {
          if (_context.UtilisateursProfils == null)
          {
              return NotFound();
          }
            return await _context.UtilisateursProfils.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/UtilisateursProfils/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateursProfil>> GetUtilisateursProfil(int id)
        {
          if (_context.UtilisateursProfils == null)
          {
              return NotFound();
          }
            var utilisateursProfil = await _context.UtilisateursProfils.Where(c => !c.EstSupprimer)
                                                             .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (utilisateursProfil == null)
            {
                return NotFound();
            }

            return utilisateursProfil;
        }

        // PUT: api/UtilisateursProfils/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUtilisateursProfil(UtilisateursProfil utilisateursProfil)
        {
            //if (id != utilisateursProfil.Id)
            //{
            //    return BadRequest();
            //}

            if (!UtilisateursProfilExists(utilisateursProfil.Id))
            {
                return NotFound();
            }
            _context.Entry(utilisateursProfil).State = EntityState.Modified;

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

        // POST: api/UtilisateursProfils
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UtilisateursProfil>> PostUtilisateursProfil(UtilisateursProfil utilisateursProfil)
        {
          if (_context.UtilisateursProfils == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.UtilisateursProfils'  is null.");
          }
            _context.UtilisateursProfils.Add(utilisateursProfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateursProfil", new { id = utilisateursProfil.Id }, utilisateursProfil);
        }

        // DELETE: api/UtilisateursProfils/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateursProfil(int id)
        {
            if (_context.UtilisateursProfils == null)
            {
                return NotFound();
            }
            var utilisateursProfil = await _context.UtilisateursProfils.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (utilisateursProfil == null)
            {
                return NotFound();
            }

            utilisateursProfil.EstSupprimer = true;
            _context.Entry(utilisateursProfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateursProfilExists(int id)
        {
            return (_context.UtilisateursProfils.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
