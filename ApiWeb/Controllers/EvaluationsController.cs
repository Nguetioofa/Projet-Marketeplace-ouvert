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
    public class EvaluationsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public EvaluationsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Evaluations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluation>>> GetEvaluations()
        {
          if (_context.Evaluations == null)
          {
              return NotFound();
          }
            return await _context.Evaluations.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Evaluations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetEvaluation(int id)
        {
          if (_context.Evaluations == null)
          {
              return NotFound();
          }
            var evaluation = await _context.Evaluations.Where(c => !c.EstSupprimer)
                                                                .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (evaluation == null)
            {
                return NotFound();
            }

            return evaluation;
        }

        // PUT: api/Evaluations/5
        [HttpPut]
        public async Task<IActionResult> PutEvaluation(Evaluation evaluation)
        {
            //if (id != evaluation.Id)
            //{
            //    return BadRequest();
            //}

            if (!EvaluationExists(evaluation.Id))
            {
                return NotFound();
            }

            _context.Entry(evaluation).State = EntityState.Modified;

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

        // POST: api/Evaluations
        [HttpPost]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        {
          if (_context.Evaluations == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Evaluations'  is null.");
          }
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluation", new { id = evaluation.Id }, evaluation);
        }

        // DELETE: api/Evaluations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            if (_context.Evaluations == null)
            {
                return NotFound();
            }
            var evaluation = await _context.Evaluations.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (evaluation == null)
            {
                return NotFound();
            }

            evaluation.EstSupprimer = true;
            _context.Entry(evaluation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluationExists(int id)
        {
            return (_context.Evaluations.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
