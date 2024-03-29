﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Users;
using SiteWeb.Services.Interfaces;
using System.Security.Claims;

namespace SiteWeb.Controllers
{
	[Authorize]
	//[ValidateAntiForgeryToken]
	public class MessagesController : Controller
	{
		private readonly IMessageService _messageService;
		public MessagesController( IMessageService messageService) 
		{ 
			_messageService = messageService;
		}
		// GET: MessagesController
		public async Task<IActionResult> Index()
		{
            int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            var listconversations = await _messageService.GetAllConversationByIdUtilisateur(idUser);
			return View(listconversations);
		}

		// GET: MessagesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

        [ValidateAntiForgeryToken]
        // GET: MessagesController/Create
        public ActionResult Create()
		{
			return View();
		}

		// POST: MessagesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int idjouet,int idUser,string message,string nature)
		{
            if (ModelState.IsValid)
            {
                int exp = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
				var corpmessage = string.Empty;

				if (!string.IsNullOrWhiteSpace(nature))
                {
					corpmessage = $"<a asp-controller=\"{nature}\" asp-action=\"Details\" asp-route-id=\"{idjouet}\"> {message} </a>";
				}
				else
				{
					corpmessage = nature;
				}

				MessageL messageL = new MessageL() { 
									Contenu = corpmessage,
									IdExpediteur = exp,
									IdDestinataire = idUser,
									Id = 0,
									DateM = DateTime.Now,
				};
				var isReussit = await _messageService.AddMessage(messageL);
                return Json(new
                {
                    date = isReussit
				});
            }
			else
			{
				return NoContent();
			}
        }


		// POST: MessagesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EnvoyerMessage(int idUser, string message)
		{
			if (!string.IsNullOrWhiteSpace(message))
			{
				int exp = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

				MessageL messageL = new MessageL()
				{
					Contenu = message,
					IdExpediteur = exp,
					IdDestinataire = idUser,
					Id = 0,
					DateM = DateTime.Now,
				};
				var isReussit = await _messageService.AddMessage(messageL);
				return Json(new
				{
					reussit = isReussit,
				});
			}
			else
			{
				return Json(new
				{
					reussit = false,
				});
			}
		}

		// GET: MessagesController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: MessagesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MessagesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: MessagesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
