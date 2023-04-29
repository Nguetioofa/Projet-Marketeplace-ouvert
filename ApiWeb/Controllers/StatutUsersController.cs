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
    public class StatutUsersController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public StatutUsersController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/StatutUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatutUser>>> GetStatutUsers()
        {
          if (_context.StatutUsers == null)
          {
              return NotFound();
          }
            return await _context.StatutUsers.Where(e => !e.EstSupprimer).ToListAsync();
        }

        // GET: api/StatutUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatutUser>> GetStatutUser(int id)
        {
          if (_context.StatutUsers == null)
          {
              return NotFound();
          }
            var statutUser = await _context.StatutUsers.Where(c => !c.EstSupprimer)
                                                             .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (statutUser == null)
            {
                return NotFound();
            }

            return statutUser;
        }

        // PUT: api/StatutUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutStatutUser(StatutUser statutUser)
        {
            //if (id != statutUser.Id)
            //{
            //    return BadRequest();
            //}
            if (!StatutUserExists(statutUser.Id))
            {
                return NotFound();
            }

            _context.Entry(statutUser).State = EntityState.Modified;

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

        // POST: api/StatutUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatutUser>> PostStatutUser(StatutUser statutUser)
        {
          if (_context.StatutUsers == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.StatutUsers'  is null.");
          }
            _context.StatutUsers.Add(statutUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatutUser", new { id = statutUser.Id }, statutUser);
        }

        // DELETE: api/StatutUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatutUser(int id)
        {
            if (_context.StatutUsers == null)
            {
                return NotFound();
            }
            var statutUser = await _context.StatutUsers.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (statutUser == null)
            {
                return NotFound();
            }

            statutUser.EstSupprimer = true;
            _context.Entry(statutUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatutUserExists(int id)
        {
            return (_context.StatutUsers.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
