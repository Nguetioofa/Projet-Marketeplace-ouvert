using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Models;
using NuGet.Packaging;
using ModelsLibrary.Models.Users;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly EchangeJouetsContext _context;

        public MessagesController(EchangeJouetsContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
          if (_context.Messages == null)
          {
              return NotFound();
          }
            return await _context.Messages.Where(ab => !ab.EstSupprimer).ToListAsync();
        }

		// GET: api/Messages/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Message>> GetMessage(int id)
		{
			if (_context.Messages == null)
			{
				return NotFound();
			}
			var message = await _context.Messages.Where(c => !c.EstSupprimer)
														 .Where(ca => ca.Id == id).FirstOrDefaultAsync();

			if (message == null)
			{
				return NotFound();
			}

			return message;
		}

		// GET: api/GetMessageByIdUtilisateur/5
		[HttpGet("GetMessageByIdUtilisateur/{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessageByIdUtilisateur(int id)
        {
          if (_context.Messages == null)
          {
              return NotFound();
          }
            var messages = await _context.Messages.Where(c => !c.EstSupprimer)
                                                         .Where(ms=> ms.IdDestinataire == id || ms.IdExpediteur == id)
                                                         .ToListAsync();

            if (messages is null)
            {
                return NotFound();
            }

            return Ok(messages);
        }


		// GET: api/GetAllConversationByIdUtilisateur/5
		[HttpGet("GetAllConversationByIdUtilisateur/{id}")]
		public async Task<ActionResult<IEnumerable<UserIdName>>> GetAllConversationByIdUtilisateur(int id)
		{
			if (_context.Messages == null)
			{
				return NotFound();
			}
            var idusers1 = await _context.Messages.Where(c => !c.EstSupprimer)
                                                         .Where(ms => ms.IdDestinataire == id || ms.IdExpediteur == id)
														 .OrderBy(m => m.DateM)
														 .Select(d => new UserIdName() { Id = d.IdDestinataireNavigation.Id,Email = d.IdDestinataireNavigation.Email,Name = d.IdDestinataireNavigation.Nom + " " + d.IdDestinataireNavigation.Prenom })
                                                         .Where(o=> o.Id != id)
														 .ToListAsync();

			 idusers1.AddRange(await _context.Messages.Where(c => !c.EstSupprimer)
											 .Where(ms => ms.IdDestinataire == id || ms.IdExpediteur == id)
											 .OrderBy(m => m.DateM)
											 .Select(d => new UserIdName() { Id = d.IdExpediteurNavigation.Id, Email = d.IdExpediteurNavigation.Email, Name = d.IdExpediteurNavigation.Nom + " " + d.IdExpediteurNavigation.Prenom })
											 .Where(o => o.Id != id)
											 .ToListAsync());

			if (idusers1 is null)
			{
				return NotFound();
			}

			return Ok(idusers1.DistinctBy(p => p.Id));
		}

		// GET: api/GetMessageByIdUtilisateur/5
		[HttpGet("GetMessageByConversation/{idUser1}/{idUser2}")]
		public async Task<ActionResult<IEnumerable<Message>>> GetMessageByConversation(int idUser1,int idUser2)
		{
			if (_context.Messages == null)
			{
				return NotFound();
			}
			var messages = await _context.Messages.Where(c => !c.EstSupprimer)
														 .Where(ms => (ms.IdDestinataire == idUser1 && ms.IdExpediteur == idUser2) || (ms.IdDestinataire == idUser2 && ms.IdExpediteur == idUser1))
														 .ToListAsync();

			if (messages is null)
			{
				return NotFound();
			}

			return Ok(messages);
		}
		// PUT: api/Messages/5
		[HttpPut]
        public async Task<IActionResult> PutMessage(Message message)
        {
            //if (id != message.Id)
            //{
            //    return BadRequest();
            //}

            if (!MessageExists(message.Id))
            {
                return NotFound();
            }

            _context.Entry(message).State = EntityState.Modified;

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

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
          if (_context.Messages == null)
          {
              return Problem("Entity set 'EchangeJouetsContext.Messages'  is null.");
          }
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (_context.Messages == null)
            {
                return NotFound();
            }
            var message = await _context.Messages.Where(e => !e.EstSupprimer && e.Id == id).FirstOrDefaultAsync();
            if (message == null)
            {
                return NotFound();
            }

            message.EstSupprimer = true;
            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return (_context.Messages.Where(e => !e.EstSupprimer)?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
