using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Models;
using ModelsLibrary.Models;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;
       

        public PhotosController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            if (_context.Photos == null)
            {
                return NotFound();
            }
            return await _context.Photos.Where(e => !e.EstSupprimer).ToListAsync();
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            if (_context.Photos == null)
            {
                return NotFound();
            }
            var photo = await _context.Photos.Where(c => !c.EstSupprimer)
                                                   .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // GET: api/GetPhotoByIdJouet/5
        [HttpGet("GetPhotoByIdJouet/{id}")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotoByIdJouet(int id)
        {
            if (_context.Photos == null)
            {
                return NotFound();
            }


            var photos = await _context.Jouets.Where(u => !u.EstSupprimer && u.Id == id)
                                                                  .Select(u => u.Photos).FirstOrDefaultAsync();
                        

            if (photos == null)
            {
                return NotFound();
            }

            return Ok(photos);
        }

		// GET: api/GetPhotoByIdAnnonce/5
		[HttpGet("GetPhotoByIdAnnonce/{id}")]
		public async Task<ActionResult<IEnumerable<Photo>>> GetPhotoByIdAnnonce(int id)
		{
			if (_context.Photos == null)
			{
				return NotFound();
			}


			var photos = await _context.Annonces.Where(u => !u.EstSupprimer && u.Id == id)
																.Select(u => u.Photos).FirstOrDefaultAsync();


			if (photos == null)
			{
				return NotFound();
			}

			return Ok(photos);
		}


		// GET: api/GetPhotoByIdProfil/5
		[HttpGet("GetPhotoByIdProfil/{id}")]
		public async Task<ActionResult<IEnumerable<Photo>>> GetPhotoByIdProfil(int id)
		{
			if (_context.Photos == null)
			{
				return NotFound();
			}


			var photos = await _context.Utilisateurs.Where(u => !u.EstSupprimer && u.Id == id)
																.Select(u => u.Photos).FirstOrDefaultAsync();


			if (photos == null)
			{
				return NotFound();
			}

			return Ok(photos);
		}


		// GET: api/GetPhotoByIdProfil/5
		[HttpGet("GetPhotoByIdMessage/{id}")]
		public async Task<ActionResult<IEnumerable<Photo>>> GetPhotoByIdMessage(int id)
		{
			if (_context.Photos == null)
			{
				return NotFound();
			}


			var photos = await _context.Messages.Where(u => !u.EstSupprimer && u.Id == id)
																.Select(u => u.Photos).FirstOrDefaultAsync();


			if (photos == null)
			{
				return NotFound();
			}

			return Ok(photos);
		}

		// PUT: api/Photos/5
		[HttpPut]
        public async Task<IActionResult> PutPhoto(Photo photo)
        {

            if (!PhotoExists(photo.Id))
            {
                return NotFound();
            }

            _context.Entry(photo).State = EntityState.Modified;

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

        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
            if (_context.Photos == null)
            {
                return Problem("Entity set 'EchangeJouetsContext.Photos'  is null.");
            }
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
        }

 

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            if (_context.Photos == null)
            {
                return NotFound();
            }
            var photo = await _context.Photos.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (photo == null)
            {
                return NotFound();
            }

            photo.EstSupprimer = true;
            _context.Entry(photo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(int id)
        {
            return (_context.Photos.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
