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
    public class AchatsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public AchatsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Achats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achat>>> GetAchats()
        {
          if (_context.Achats == null)
          {
              return NotFound();
          }
            return await _context.Achats.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Achats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Achat>> GetAchat(int id)
        {
          if (_context.Achats == null)
          {
              return NotFound();
          }
            var achat = await _context.Achats.Where(c => !c.EstSupprimer)
                                                   .Where(ca => ca.Id == id).FirstOrDefaultAsync();


            if (achat == null)
            {
                return NotFound();
            }

            return achat;
        }

        // PUT: api/Achats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutAchat(Achat achat)
        {
            //if (id != achat.Id)
            //{
            //    return BadRequest();
            //}

            if (!AchatExists(achat.Id))
            {
                return NotFound();
            }

            _context.Entry(achat).State = EntityState.Modified;

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

        // POST: api/Achats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Achat>> PostAchat(Achat achat)
        {
            if (_context.Achats == null)
            {
                return Problem("Entity set 'EchangeJouetsContext.Achats'  is null.");
            }
            _context.Achats.Add(achat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAchat", new { id = achat.Id }, achat);
        }

        // DELETE: api/Achats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchat(int id)
        {
            if (_context.Achats == null)
            {
                return NotFound();
            }
            var achat = await _context.Achats.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (achat == null)
            {
                return NotFound();
            }

            achat.EstSupprimer = true;
            _context.Entry(achat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AchatExists(int id)
        {
            return (_context.Achats.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
