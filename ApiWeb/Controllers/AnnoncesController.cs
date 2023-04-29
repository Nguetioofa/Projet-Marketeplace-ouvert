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
    public class AnnoncesController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public AnnoncesController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Annonces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Annonce>>> GetAnnonces()
        {
          if (_context.Annonces == null)
          {
              return NotFound();
          }
            return Ok(await _context.Annonces.Where(ab => !ab.EstSupprimer).ToListAsync());
        }

        // GET: api/Annonces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Annonce>> GetAnnonce(int id)
        {
          if (_context.Annonces == null)
          {
              return NotFound();
          }
            var annonce = await _context.Annonces.Where(c => !c.EstSupprimer)
                                                        .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (annonce == null)
            {
                return NotFound();
            }

            return Ok(annonce);
        }

        // PUT: api/Annonces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutAnnonce(Annonce annonce)
        {
            //if (id != annonce.Id)
            //{
            //    return BadRequest();
            //}
            if (!AnnonceExists(annonce.Id))
            {
                return NotFound();
            }

            try
            {
                _context.Entry(annonce).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                    throw;

            }

            return NoContent();
        }

        // POST: api/Annonces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Annonce>> PostAnnonce(Annonce annonce)
        {
            if (_context.Annonces == null)
            {
                return Problem("Entity set 'EchangeJouetsContext.Annonces'  is null.");
            }
            _context.Annonces.Add(annonce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnonce", new { id = annonce.Id }, annonce);
        }

        // DELETE: api/Annonces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnonce(int id)
        {
            if (_context.Annonces == null)
            {
                return NotFound();
            }
            var annonce = await _context.Annonces.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (annonce == null)
            {
                return NotFound();
            }

            annonce.EstSupprimer = true;
            _context.Entry(annonce).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnonceExists(int id)
        {
            return (_context.Annonces.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
