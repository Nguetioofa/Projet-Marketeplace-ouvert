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
    public class NewslettersController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public NewslettersController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Newsletters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Newsletter>>> GetNewsletters()
        {
          if (_context.Newsletters == null)
          {
              return NotFound();
          }
            return await _context.Newsletters.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Newsletters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Newsletter>> GetNewsletter(int id)
        {
          if (_context.Newsletters == null)
          {
              return NotFound();
          }
            var newsletter = await _context.Newsletters.Where(c => !c.EstSupprimer)
                                                                 .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (newsletter == null)
            {
                return NotFound();
            }

            return newsletter;
        }

        // PUT: api/Newsletters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutNewsletter(Newsletter newsletter)
        {
            //if (id != newsletter.Id)
            //{
            //    return BadRequest();
            //}
            if (!NewsletterExists(newsletter.Id))
            {
                return NotFound();
            }
            _context.Entry(newsletter).State = EntityState.Modified;

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

        // POST: api/Newsletters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Newsletter>> PostNewsletter(Newsletter newsletter)
        {
          if (_context.Newsletters == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Newsletters'  is null.");
          }
            _context.Newsletters.Add(newsletter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsletter", new { id = newsletter.Id }, newsletter);
        }

        // DELETE: api/Newsletters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsletter(int id)
        {
            if (_context.Newsletters == null)
            {
                return NotFound();
            }
            var newsletter = await _context.Newsletters.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (newsletter == null)
            {
                return NotFound();
            }

            newsletter.EstSupprimer = true;
            _context.Entry(newsletter).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsletterExists(int id)
        {
            return (_context.Newsletters.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
