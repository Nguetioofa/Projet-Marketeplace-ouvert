using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace ApiWeb.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JouetsController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public JouetsController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Jouets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jouet>>> GetJouets()
        {
          if (_context.Jouets == null)
          {
              return NotFound();
          }
            return await _context.Jouets.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

		// GET: api/Jouets
		[HttpGet("GetJouetsByIdCategorie/{id}")]
		public async Task<ActionResult<IEnumerable<Jouet>>> GetJouetsByIdCategorie(int id)
		{
			if (_context.Jouets == null)
			{
				return NotFound();
			}
			return await _context.Jouets.Where(ab => !ab.EstSupprimer && ab.Categorie == id).ToListAsync();
		}

		// GET: api/Jouets
		[HttpGet("GetJouetsByNameCategorie/{name}")]
		public async Task<ActionResult<IEnumerable<Jouet>>> GetJouetsByNameCategorie(string name)
		{
			if (_context.Jouets == null)
			{
				return NotFound();
			}
            var idCat = _context.CategorieJouets.Where(c => !c.EstSupprimer && c.Nom==name)
                                                             .Select(c=>c.Id).FirstOrDefault();
            if (idCat == 0)
                return NotFound();

            var jouets = await _context.Jouets.Where(ab => !ab.EstSupprimer && ab.Categorie == idCat)
                                        .ToListAsync();

			if (jouets is null)
				return NotFound();

			return Ok(jouets);
		}

		// GET: api/Jouets
		[HttpGet("GetJouetsByName/{name}")]
		public async Task<ActionResult<IEnumerable<Jouet>>> GetJouetsByName(string name)
		{
			if (_context.Jouets == null)
			{
				return NotFound();
			}

			var jouets = await _context.Jouets.Where(ab => !ab.EstSupprimer && 
                                                        ab.Nom.Contains(name))
										                .ToListAsync();

			if (jouets is null)
				return NotFound();

			return Ok(jouets);
		}

		// GET: api/Jouets/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Jouet>> GetJouet(int id)
        {
          if (_context.Jouets == null)
          {
              return NotFound();
          }
            var jouet = await _context.Jouets.Where(c => !c.EstSupprimer)
                                                   .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (jouet == null)
            {
                return NotFound();
            }

            return jouet;
        }

        // GET: api/Jouets/JouetByIdCategorie/5
        [HttpGet("GetJouetByIdCategorie{id}"), Authorize]
        public async Task<ActionResult<IEnumerable<Jouet>>> GetJouetsByJouetByIdCategorieJouet(int id)
        {
            if (_context.Jouets == null)
            {
                return NotFound();
            }

            var jouets = await _context.Jouets.Where(c => c.Categorie == id)
                                                .Where(c => !c.EstSupprimer)
                                                .ToListAsync();
            if (jouets == null)
            {
                return NotFound();
            }

            return jouets;
        }

        // GET: api/Jouets/JouetByIdUser/5
        [HttpGet("GetJoutsByIdUtilisateur/{id}")]
        public async Task<ActionResult<IEnumerable<Jouet>>> GetJouetsByJouetByIdUser(int id)
        {
            if (_context.Jouets == null)
            {
                return NotFound();
            }

           var jouets = await  _context.Jouets.Where(j => j.Proprietaire == id)
                            .Where(j => !j.EstSupprimer)
                            .ToListAsync();

            if (jouets == null)
            {
                return NotFound();
            }

            return jouets;
        }

        // PUT: api/Jouets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutJouet(Jouet jouet)
        {
            //if (id != jouet.Id)
            //{
            //    return BadRequest();
            //}

            if (!JouetExists(jouet.Id))
            {
                return NotFound();
            }

            _context.Entry(jouet).State = EntityState.Modified;

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

        // POST: api/Jouets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jouet>> PostJouet(Jouet jouet)
        {
          if (_context.Jouets == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Jouets'  is null.");
          }
            _context.Jouets.Add(jouet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJouet", new { id = jouet.Id }, jouet);
        }

        // DELETE: api/Jouets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJouet(int id)
        {
            if (_context.Jouets == null)
            {
                return NotFound();
            }
            var jouet = await _context.Jouets.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (jouet == null)
            {
                return NotFound();
            }

            jouet.EstSupprimer = true;
            _context.Entry(jouet).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[HttpGet("GetJoutsByIdUtilisateur{id}")]
        //public async Task<ActionResult<IEnumerable<Jouet>>> GetJoutsByIdUtilisateur(int id)
        //{
        //    if (_context.Utilisateurs == null || _context.Jouets == null)
        //    {
        //        return NotFound();
        //    }

        //    var Jouets = await _context.Jouets.Where(j => !j.EstSupprimer)
        //                                       .Join(_context.Utilisateurs.Where(u => !u.EstSupprimer && u.Id == id),
        //                                       jouet => jouet.Proprietaire,
        //                                       user => user.Id,
        //                                       (jouet , user) => jouet).ToListAsync();
        //    if (Jouets == null)
        //        return NotFound();

        //    return Ok(Jouets);
        //}
        private bool JouetExists(int id)
        {
            return (_context.Jouets.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
