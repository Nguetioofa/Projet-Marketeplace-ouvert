using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Echanges;
using SiteWeb.Services.Interfaces;
using System.Security.Claims;

namespace SiteWeb.Controllers
{
	[Authorize]
	public class EchangesController : Controller
	{
		private readonly IEchangeService _echangeService;

		public EchangesController(IEchangeService echangeService)
		{
            _echangeService = echangeService;
        }

        // GET: EchangesController
        [HttpGet]
        public ActionResult Index()
		{
			return View();
		}

		// GET: EchangesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

        [HttpPost]
        // GET: EchangesController/Create
        public ActionResult Create(int iduser1, int idjouet1)
		{
            ViewBag.iduser1 = iduser1;
			ViewBag.idjouet1 = idjouet1;
            return View();
		}

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifiedStatut(int idechange, int idstatut)
        {
            var succes = await _echangeService.ChangeStatutTransaction(idechange, idstatut);

            if (succes)
            return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = "une erreur s'est produite au changement du statut";
            return RedirectToAction(nameof(Index));

        }


        // POST: EchangesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] EchangeL echangeL)
		{
			try
			{
                int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
				echangeL.IdUtilisateur2 = idUser;
				echangeL.ModeLivraison1 = 1;
				echangeL.ModePayUtilisateur1 = 1;
                Guid newGuid = Guid.NewGuid();
                echangeL.Reference = newGuid.ToString();
                echangeL.Statut = 1;
				//echangeL.DateInit = DateTime.UtcNow;
				var sussses =  await _echangeService.AddEchange(echangeL);
                if (!sussses)
                {
                    ViewBag.iduser1 = echangeL.IdUtilisateur1;
                    ViewBag.idjouet1 = echangeL.Jouet1;
                    ViewBag.ErrorMessage = "une erreur s'est produite";
                    return View();
                }

				return RedirectToAction(nameof(Index));
 
            }
            catch
			{
				ViewBag.ErrorMessage = "une erreur s'est produite";
                return View();
			}

		}

		// GET: EchangesController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: EchangesController/Edit/5
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

		// GET: EchangesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: EchangesController/Delete/5
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
