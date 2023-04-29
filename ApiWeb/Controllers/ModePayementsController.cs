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
    public class ModePayementsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public ModePayementsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/ModePayements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModePayement>>> GetModePayements()
        {
          if (_context.ModePayements == null)
          {
              return NotFound();
          }
            return await _context.ModePayements.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/ModePayements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModePayement>> GetModePayement(int id)
        {
          if (_context.ModePayements == null)
          {
              return NotFound();
          }
            var modePayement = await _context.ModePayements.Where(c => !c.EstSupprimer)
                                                                      .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (modePayement == null)
            {
                return NotFound();
            }

            return modePayement;
        }

        // PUT: api/ModePayements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutModePayement(ModePayement modePayement)
        {
            //if (id != modePayement.Id)
            //{
            //    return BadRequest();
            //}
            if (!ModePayementExists(modePayement.Id))
            {
                return NotFound();
            }

            _context.Entry(modePayement).State = EntityState.Modified;

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

        // POST: api/ModePayements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModePayement>> PostModePayement(ModePayement modePayement)
        {
          if (_context.ModePayements == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.ModePayements'  is null.");
          }
            _context.ModePayements.Add(modePayement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModePayement", new { id = modePayement.Id }, modePayement);
        }

        // DELETE: api/ModePayements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModePayement(int id)
        {
            if (_context.ModePayements == null)
            {
                return NotFound();
            }
            var modePayement = await _context.ModePayements.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (modePayement == null)
            {
                return NotFound();
            }

            modePayement.EstSupprimer = true;
            _context.Entry(modePayement).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModePayementExists(int id)
        {
            return (_context.ModePayements.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
