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
    public class MessagesPhotoesController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public MessagesPhotoesController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/MessagesPhotoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessagesPhoto>>> GetMessagesPhotos()
        {
            if (_context.MessagesPhotos == null)
            {
                return NotFound();
            }
            return await _context.MessagesPhotos.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

        // GET: api/MessagesPhotoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessagesPhoto>> GetMessagesPhoto(int id)
        {
            if (_context.MessagesPhotos == null)
            {
                return NotFound();
            }
            var messagesPhoto = await _context.MessagesPhotos.Where(c => !c.EstSupprimer)
                                                                        .Where(ca => ca.Id == id).FirstOrDefaultAsync();

            if (messagesPhoto == null)
            {
                return NotFound();
            }

            return messagesPhoto;
        }

        // PUT: api/MessagesPhotoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessagesPhoto(int id, MessagesPhoto messagesPhoto)
        {
            //if (id != messagesPhoto.Id)
            //{
            //    return BadRequest();
            //}

            if (!MessagesPhotoExists(id))
            {
                return NotFound();
            }

            _context.Entry(messagesPhoto).State = EntityState.Modified;

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

        // POST: api/MessagesPhotoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessagesPhoto>> PostMessagesPhoto(MessagesPhoto messagesPhoto)
        {
            if (_context.MessagesPhotos == null)
            {
                return Problem("Entity set 'EchangeJouetsContext.MessagesPhotos'  is null.");
            }
            _context.MessagesPhotos.Add(messagesPhoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessagesPhoto", new { id = messagesPhoto.Id }, messagesPhoto);
        }

        // DELETE: api/MessagesPhotoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessagesPhoto(int id)
        {
            if (_context.MessagesPhotos == null)
            {
                return NotFound();
            }
            var messagesPhoto = await _context.MessagesPhotos.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (messagesPhoto == null)
            {
                return NotFound();
            }

            messagesPhoto.EstSupprimer = true;
            _context.Entry(messagesPhoto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessagesPhotoExists(int id)
        {
            return (_context.MessagesPhotos.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
