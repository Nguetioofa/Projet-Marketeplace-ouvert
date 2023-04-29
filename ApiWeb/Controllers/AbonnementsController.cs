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
    public class AbonnementsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public AbonnementsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Abonnements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnements()
        {
          if (_context.Abonnements == null)
          {
              return NotFound();
          }
            return await _context.Abonnements.Where(ab=>!ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Abonnements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Abonnement>> GetAbonnement(int id)
        {
          if (_context.Abonnements == null)
          {
              return NotFound();
          }
            var abonnement = await _context.Abonnements
                                                     .Where(c => !c.EstSupprimer)
                                                     .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (abonnement == null)
            {
                return NotFound();
            }

            return Ok(abonnement);
        }

        // PUT: api/Abonnements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutAbonnement(Abonnement abonnement)
        {
            //if (id != abonnement.Id)
            //{
            //    return BadRequest();
            //}
            if (!AbonnementExists(abonnement.Id))
            {
                return NotFound();
            }
            _context.Entry(abonnement).State = EntityState.Modified;

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

        // POST: api/Abonnements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Abonnement>> PostAbonnement(Abonnement abonnement)
        {
            if (_context.Abonnements == null)
            {
                return Problem("Entity set 'EchangeJouetsContext.Abonnements'  is null.");
            }
            _context.Abonnements.Add(abonnement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbonnement", new { id = abonnement.Id }, abonnement);
        }

        // DELETE: api/Abonnements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbonnement(int id)
        {
            if (_context.Abonnements == null)
            {
                return NotFound();
            }
            var abonnement = await _context.Abonnements.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (abonnement == null)
            {
                return NotFound();
            }

            abonnement.EstSupprimer = true;

            _context.Entry(abonnement).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbonnementExists(int id)
        {
            return (_context.Abonnements.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
