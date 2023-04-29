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
    public class ModeLivraisonsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public ModeLivraisonsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/ModeLivraisons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModeLivraison>>> GetModeLivraisons()
        {
            if (_context.ModeLivraisons == null)
            {
                return NotFound();
            }
            return await _context.ModeLivraisons.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/ModeLivraisons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModeLivraison>> GetModeLivraison(int id)
        {
            if (_context.ModeLivraisons == null)
            {
                return NotFound();
            }
            var modeLivraison = await _context.ModeLivraisons.Where(c => !c.EstSupprimer)
                                                                        .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (modeLivraison == null)
            {
                return NotFound();
            }

            return modeLivraison;
        }

        // PUT: api/ModeLivraisons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutModeLivraison(ModeLivraison modeLivraison)
        {
            //if (id != modeLivraison.Id)
            //{
            //    return BadRequest();
            //}

            if (!ModeLivraisonExists(modeLivraison.Id))
            {
                return NotFound();
            }
            _context.Entry(modeLivraison).State = EntityState.Modified;

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

        // POST: api/ModeLivraisons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModeLivraison>> PostModeLivraison(ModeLivraison modeLivraison)
        {
            if (_context.ModeLivraisons == null)
            {
                return Problem("Entity set 'EchangeJouetsContext.ModeLivraisons'  is null.");
            }
            _context.ModeLivraisons.Add(modeLivraison);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModeLivraison", new { id = modeLivraison.Id }, modeLivraison);
        }

        // DELETE: api/ModeLivraisons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModeLivraison(int id)
        {
            if (_context.ModeLivraisons == null)
            {
                return NotFound();
            }
            var modeLivraison = await _context.ModeLivraisons.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (modeLivraison == null)
            {
                return NotFound();
            }

            modeLivraison.EstSupprimer = true;
            _context.Entry(modeLivraison).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModeLivraisonExists(int id)
        {
            return (_context.ModeLivraisons.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
