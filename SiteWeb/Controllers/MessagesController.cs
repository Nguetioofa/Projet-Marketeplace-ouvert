using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteWeb.Services.Interfaces;
using System.Security.Claims;

namespace SiteWeb.Controllers
{
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

		// GET: MessagesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: MessagesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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
