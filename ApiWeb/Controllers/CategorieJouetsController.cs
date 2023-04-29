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
    public class CategorieJouetsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public CategorieJouetsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/CategorieJouets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategorieJouet>>> GetCategorieJouets()
        {
          if (_context.CategorieJouets == null)
          {
              return NotFound();
          }
            return await _context.CategorieJouets.Where(c=> !c.EstSupprimer).ToListAsync();
        }

        // GET: api/CategorieJouets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategorieJouet>> GetCategorieJouet(int id)
        {
          if (_context.CategorieJouets == null)
          {
              return NotFound();
          }
            var categorieJouet = await _context.CategorieJouets
                                                               .Where(c => !c.EstSupprimer)
                                                               .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (categorieJouet == null)
            {
                return NotFound();
            }

            return categorieJouet;
        }

        // PUT: api/CategorieJouets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCategorieJouet(CategorieJouet categorieJouet)
        {
            //if (id != categorieJouet.Id)
            //{
            //    return BadRequest();
            //}

            if (!CategorieJouetExists(categorieJouet.Id))
            {
                return NotFound();
            }

            _context.Entry(categorieJouet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CategorieJouetExists(categorieJouet.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }

            return NoContent();
        }

        // POST: api/CategorieJouets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategorieJouet>> PostCategorieJouet(CategorieJouet categorieJouet)
        {
          if (_context.CategorieJouets == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.CategorieJouets'  is null.");
          }
            _context.CategorieJouets.Add(categorieJouet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorieJouet", new { id = categorieJouet.Id }, categorieJouet);
        }

        // DELETE: api/CategorieJouets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorieJouet(int id)
        {
            if (_context.CategorieJouets == null)
            {
                return NotFound();
            }
            var categorieJouet = await _context.CategorieJouets.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (categorieJouet == null)
            {
                return NotFound();
            }
            categorieJouet.EstSupprimer = true;
            _context.Entry(categorieJouet).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategorieJouetExists(int id)
        {
            return (_context.CategorieJouets.Where(e=>!e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
