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
    public class JouetsPhotoesController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public JouetsPhotoesController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/JouetsPhotoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JouetsPhoto>>> GetJouetsPhotos()
        {
          if (_context.JouetsPhotos == null)
          {
              return NotFound();
          }
            return await _context.JouetsPhotos.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/JouetsPhotoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JouetsPhoto>> GetJouetsPhoto(int id)
        {
          if (_context.JouetsPhotos == null)
          {
              return NotFound();
          }
            var jouetsPhoto = await _context.JouetsPhotos.Where(c => !c.EstSupprimer)
                                                             .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (jouetsPhoto == null)
            {
                return NotFound();
            }

            return jouetsPhoto;
        }

        // PUT: api/JouetsPhotoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutJouetsPhoto(JouetsPhoto jouetsPhoto)
        {
            //if (id != jouetsPhoto.Id)
            //{
            //    return BadRequest();
            //}

            if (!JouetsPhotoExists(jouetsPhoto.Id))
            {
                return NotFound();
            }
            _context.Entry(jouetsPhoto).State = EntityState.Modified;

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

        // POST: api/JouetsPhotoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JouetsPhoto>> PostJouetsPhoto(JouetsPhoto jouetsPhoto)
        {
          if (_context.JouetsPhotos == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.JouetsPhotos'  is null.");
          }
            _context.JouetsPhotos.Add(jouetsPhoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJouetsPhoto", new { id = jouetsPhoto.Id }, jouetsPhoto);
        }

        // DELETE: api/JouetsPhotoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJouetsPhoto(int id)
        {
            if (_context.JouetsPhotos == null)
            {
                return NotFound();
            }
            var jouetsPhoto = await _context.JouetsPhotos.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (jouetsPhoto == null)
            {
                return NotFound();
            }

            jouetsPhoto.EstSupprimer = true;
            _context.Entry(jouetsPhoto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JouetsPhotoExists(int id)
        {
            return (_context.JouetsPhotos.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
