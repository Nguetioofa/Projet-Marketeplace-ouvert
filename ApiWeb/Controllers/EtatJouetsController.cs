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
    public class EtatJouetsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public EtatJouetsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/EtatJouets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EtatJouet>>> GetEtatJouets()
        {
          if (_context.EtatJouets == null)
          {
              return NotFound();
          }
            return await _context.EtatJouets.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/EtatJouets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EtatJouet>> GetEtatJouet(int id)
        {
          if (_context.EtatJouets == null)
          {
              return NotFound();
          }
            var etatJouet = await _context.EtatJouets.Where(c => !c.EstSupprimer)
                                                             .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (etatJouet == null)
            {
                return NotFound();
            }

            return etatJouet;
        }

        // PUT: api/EtatJouets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEtatJouet(EtatJouet etatJouet)
        {
            //if (id != etatJouet.Id)
            //{
            //    return BadRequest();
            //}

            if (!EtatJouetExists(etatJouet.Id))
            {
                return NotFound();
            }
            _context.Entry(etatJouet).State = EntityState.Modified;

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

        // POST: api/EtatJouets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EtatJouet>> PostEtatJouet(EtatJouet etatJouet)
        {
          if (_context.EtatJouets == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.EtatJouets'  is null.");
          }
            _context.EtatJouets.Add(etatJouet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtatJouet", new { id = etatJouet.Id }, etatJouet);
        }

        // DELETE: api/EtatJouets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtatJouet(int id)
        {
            if (_context.EtatJouets == null)
            {
                return NotFound();
            }
            var etatJouet = await _context.EtatJouets.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (etatJouet == null)
            {
                return NotFound();
            }
            etatJouet.EstSupprimer = true;
            _context.Entry(etatJouet).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtatJouetExists(int id)
        {
            return (_context.EtatJouets.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
