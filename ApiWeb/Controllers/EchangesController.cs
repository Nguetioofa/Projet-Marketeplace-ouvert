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
    public class EchangesController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public EchangesController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Echanges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Echange>>> GetEchanges()
        {
          if (_context.Echanges == null)
          {
              return NotFound();
          }
            return await _context.Echanges.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Echanges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Echange>> GetEchange(int id)
        {
          if (_context.Echanges == null)
          {
              return NotFound();
          }
            var echange = await _context.Echanges.Where(c => !c.EstSupprimer)
                                                     .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (echange == null)
            {
                return NotFound();
            }

            return echange;
        }

        // GET: api/Echanges/5
        [HttpGet("GetEchangesByIdUser/{id}")]
        public async Task<ActionResult<IEnumerable<Echange>>> GetEchangesByIdUser(int id)
        {
            if (_context.Echanges == null)
            {
                return NotFound();
            }
            var echange = await _context.Echanges.Where(c => !c.EstSupprimer)
                                                     .Where(ca => ca.IdUtilisateur1 == id || ca.IdUtilisateur2 == id).ToListAsync();

            if (echange == null)
            {
                return NotFound();
            }

            return Ok(echange);
        }

        // PUT: api/Echanges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEchange(Echange echange)
        {
            //if (id != echange.Id)
            //{
            //    return BadRequest();
            //}
            if (!EchangeExists(echange.Id))
            {
                return NotFound();
            }
            _context.Entry(echange).State = EntityState.Modified;

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

        // PUT: api/Echanges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ChangeStatutTransaction/{idechange}/{idstatut}")]
        public async Task<IActionResult> ChangeStatutTransaction(int idechange,int idstatut)
        {
            //if (id != echange.Id)
            //{
            //    return BadRequest();
            //}

            var echange = _context.Echanges.Where(e=>!e.EstSupprimer)
                                                            .Where(e=>e.Id == idechange)
                                                            .FirstOrDefault();

            if (echange is null)
            {
                return NotFound();
            }
            echange.Statut = idstatut;
            _context.Entry(echange).State = EntityState.Modified;

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

        // POST: api/Echanges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Echange>> PostEchange(Echange echange)
        {
          if (_context.Echanges == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Echanges'  is null.");
          }
            _context.Echanges.Add(echange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEchange", new { id = echange.Id }, echange);
        }

        // DELETE: api/Echanges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEchange(int id)
        {
            if (_context.Echanges == null)
            {
                return NotFound();
            }
            var echange = await _context.Echanges.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (echange == null)
            {
                return NotFound();
            }

            echange.EstSupprimer = true;
            _context.Entry(echange).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EchangeExists(int id)
        {
            return (_context.Echanges.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
