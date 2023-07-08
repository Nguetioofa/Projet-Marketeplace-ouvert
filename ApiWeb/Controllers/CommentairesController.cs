using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Models;
using NuGet.Packaging.Signing;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentairesController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public CommentairesController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Commentaires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaires()
        {
          if (_context.Commentaires == null)
          {
              return NotFound();
          }
            return await _context.Commentaires.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/Commentaires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commentaire>> GetCommentaire(int id)
        {
          if (_context.Commentaires == null)
          {
              return NotFound();
          }
            var commentaire = await _context.Commentaires.Where(c => !c.EstSupprimer)
                                                     .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

		// GET: api/GetCommentaireByIdJouet/5
		[HttpGet("GetCommentaireByIdJouet/{id}")]
		public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaireByIdJouet(int id)
		{
			if (_context.Commentaires == null)
			{
				return NotFound();
			}

            var commentaires = await _context.Jouets.Where(c => !c.EstSupprimer)
                                                      .Where(c => c.Id == id)
                                                      .Select(j => j.Commentaires).FirstOrDefaultAsync();

			if (commentaires == null)
			{
				return NotFound();
			}

			return Ok(commentaires);
		}

		// GET: api/GetCommentaireByIdAnnonce/5
		[HttpGet("GetCommentaireByIdAnnonce/{id}")]
		public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaireByIdAnnonce(int id)
		{
			if (_context.Commentaires == null)
			{
				return NotFound();
			}

			var commentaires = await _context.Annonces.Where(c => !c.EstSupprimer)
													  .Where(c => c.Id == id)
													  .Select(j => j.Commentaires).FirstOrDefaultAsync();

			if (commentaires == null)
			{
				return NotFound();
			}

			return Ok(commentaires);
		}

		// PUT: api/Commentaires/5
		[HttpPut]
        public async Task<IActionResult> PutCommentaire(Commentaire commentaire)
        {
            //if (id != commentaire.Id)
            //{
            //    return BadRequest();
            //}
            if (!CommentaireExists(commentaire.Id))
            {
                return NotFound();
            }

            _context.Entry(commentaire).State = EntityState.Modified;

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

        // POST: api/Commentaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commentaire>> PostCommentaire(Commentaire commentaire)
        {
          if (_context.Commentaires == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Commentaires'  is null.");
          }
            _context.Commentaires.Add(commentaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentaire", new { id = commentaire.Id }, commentaire);
        }

        // DELETE: api/Commentaires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentaire(int id)
        {
            if (_context.Commentaires == null)
            {
                return NotFound();
            }
            var commentaire = await _context.Commentaires.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (commentaire == null)
            {
                return NotFound();
            }

            commentaire.EstSupprimer = true;
            _context.Entry(commentaire).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentaireExists(int id)
        {
            return (_context.Commentaires?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
