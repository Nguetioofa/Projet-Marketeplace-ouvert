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
    public class StatutsTransactionsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public StatutsTransactionsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/StatutsTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatutsTransaction>>> GetStatutsTransactions()
        {
          if (_context.StatutsTransactions == null)
          {
              return NotFound();
          }
            return await _context.StatutsTransactions.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/StatutsTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatutsTransaction>> GetStatutsTransaction(int id)
        {
          if (_context.StatutsTransactions == null)
          {
              return NotFound();
          }
            var statutsTransaction = await _context.StatutsTransactions
                                                            .Where(c => !c.EstSupprimer)
                                                            .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (statutsTransaction == null)
            {
                return NotFound();
            }

            return statutsTransaction;
        }

        // PUT: api/StatutsTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatutsTransaction(StatutsTransaction statutsTransaction)
        {
            //if (id != statutsTransaction.Id)
            //{
            //    return BadRequest();
            //}

            if (!StatutsTransactionExists(statutsTransaction.Id))
            {
                return NotFound();
            }
            _context.Entry(statutsTransaction).State = EntityState.Modified;

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

        // POST: api/StatutsTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatutsTransaction>> PostStatutsTransaction(StatutsTransaction statutsTransaction)
        {
          if (_context.StatutsTransactions == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.StatutsTransactions'  is null.");
          }
            _context.StatutsTransactions.Add(statutsTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatutsTransaction", new { id = statutsTransaction.Id }, statutsTransaction);
        }

        // DELETE: api/StatutsTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatutsTransaction(int id)
        {
            if (_context.StatutsTransactions == null)
            {
                return NotFound();
            }
            var statutsTransaction = await _context.StatutsTransactions.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (statutsTransaction == null)
            {
                return NotFound();
            }

            statutsTransaction.EstSupprimer = true;
            _context.Entry(statutsTransaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatutsTransactionExists(int id)
        {
            return (_context.StatutsTransactions.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
