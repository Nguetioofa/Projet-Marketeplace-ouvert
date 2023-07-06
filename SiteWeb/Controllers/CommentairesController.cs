using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using System.Security.Claims;

namespace SiteWeb.Controllers
{

	public class CommentairesController : Controller
	{
		private readonly ICommentaireService _commentaireService;

		public CommentairesController(ICommentaireService commentaireService)
		{
			_commentaireService = commentaireService;
		}
		// GET: CommentairesController
		public ActionResult Index()
		{
			return View();
		}

		// GET: CommentairesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		//[Authorize]
		//[HttpGet]
		//public IActionResult Create()
		//{
		//	return View();
		//}

		// GET: CommentairesController/Create
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] string commentaire)
		{
            if (ModelState.IsValid)
            {
				int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
				var commantaire = new CommentaireL()
				{
					Contenu = commentaire,
					DateC = DateTime.UtcNow,
					IdAuteur = idUser,
					IdAnnonce = null,
					IdJouet = 35
				};
				await _commentaireService.AddCommentaire(commantaire);
				return RedirectToAction("Details", "Jouets", routeValues: "35");

			}
			return RedirectToAction("Details", "Jouets", routeValues: "35");

		}

		// POST: CommentairesController/Create
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

	}
}
