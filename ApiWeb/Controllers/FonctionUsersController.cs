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
    public class FonctionUsersController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public FonctionUsersController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/FonctionUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FonctionUser>>> GetFonctionUsers()
        {
          if (_context.FonctionUsers == null)
          {
              return NotFound();
          }
            return await _context.FonctionUsers.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/FonctionUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FonctionUser>> GetFonctionUser(int id)
        {
          if (_context.FonctionUsers == null)
          {
              return NotFound();
          }
            var fonctionUser = await _context.FonctionUsers.Where(c => !c.EstSupprimer)
                                                             .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (fonctionUser == null)
            {
                return NotFound();
            }

            return fonctionUser;
        }

        // PUT: api/FonctionUsers/5
        [HttpPut]
        public async Task<IActionResult> PutFonctionUser(FonctionUser fonctionUser)
        {
            //if (id != fonctionUser.Id)
            //{
            //    return BadRequest();
            //}

            if (!FonctionUserExists(fonctionUser.Id))
            {
                return NotFound();
            }

            _context.Entry(fonctionUser).State = EntityState.Modified;

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

        // POST: api/FonctionUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FonctionUser>> PostFonctionUser(FonctionUser fonctionUser)
        {
          if (_context.FonctionUsers == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.FonctionUsers'  is null.");
          }
            _context.FonctionUsers.Add(fonctionUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFonctionUser", new { id = fonctionUser.Id }, fonctionUser);
        }

        // DELETE: api/FonctionUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFonctionUser(int id)
        {
            if (_context.FonctionUsers == null)
            {
                return NotFound();
            }
            var fonctionUser = await _context.FonctionUsers.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (fonctionUser == null)
            {
                return NotFound();
            }

            _context.FonctionUsers.Remove(fonctionUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FonctionUserExists(int id)
        {
            return (_context.FonctionUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
