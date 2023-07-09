using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using System.Security.Claims;
using ModelsLibrary.Models.Commentaires;

namespace SiteWeb.Controllers
{

	public class CommentairesController : Controller
	{
		private readonly ICommentaireService _commentaireService;
		private readonly IUtilisateurService _utilisateurService;

		public CommentairesController(IUtilisateurService utilisateurService, ICommentaireService commentaireService)
		{
			_utilisateurService = utilisateurService;
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
		public async Task<IActionResult> Create(string typeCommentaire, int id,string commentaire)
		{
            if (ModelState.IsValid)
            {
				int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

				var commantaire = new CommentaireL()
				{
					Contenu = commentaire,
					DateC = DateTime.UtcNow,
					IdAuteur = idUser,
				};
				if (typeCommentaire.Equals("jouet"))
				{
					commantaire.IdJouet = id;
					commantaire.IdAnnonce = null;
				}
				else if(typeCommentaire.Equals("annonce"))
				{
                    commantaire.IdAnnonce = id;
					commantaire.IdJouet = null;
				}
				var nouveaucom = await _commentaireService.AddCommentaire(commantaire);
				var username = await _utilisateurService.GetUtilisateur((int)commantaire.IdAuteur);
				//return Ok(nouveaucom);
				return Json(new
				{
					date = commantaire.DateC.ToString("dd/MM/yyyy"),
					auteur = username.Nom + " " + username.Prenom,
					contenu = commantaire.Contenu
				});

			}
			return NoContent();

		}



	}
}
