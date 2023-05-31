using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Interfaces;

namespace SiteWeb.Controllers
{
    public class JouetsController : Controller
    {
        private readonly IJouetService _jouetService;
        private readonly IPhotoService _photoService;
        private readonly IJouetsPhotoService _jouetsPhotoService;
        private readonly IEtatJouetService _etatService;
        private readonly ICategorieJouetService _categorieJouetService;
        public JouetsController( IJouetService jouetService,IPhotoService photoService
            , IJouetsPhotoService jouetsPhotoService, IEtatJouetService etatJouetService
            , ICategorieJouetService categorieJouetService) 
        {
            _jouetService = jouetService;
            _photoService = photoService;
            _jouetsPhotoService = jouetsPhotoService;
            _categorieJouetService = categorieJouetService;
            _etatService = etatJouetService;
        }

        // GET: ToysController
        public async Task<IActionResult> Index()
        {
            var jouets = (await _jouetService.GetJouets()).Value; 
            var etat = (await _etatService.GetEtatJouets());
            var categorie = (await _categorieJouetService.GetCategorieJouets()).Value;
           // var photo = (await _photoService.GetPhotos()).Value;

            List<Toy> toys = new List<Toy>();

            jouets.ForEach(async jouet => 
            {
                toys.Add(new Toy()
                {
                    Id = jouet.Id,
                    Nom = jouet.Nom,
                    Categorie = jouet.Categorie,
                    AgeMin = jouet.AgeMin,
                    AgeMax = jouet.AgeMax,
                    EtatId = jouet.EtatId,
                    Descriptions = jouet.Descriptions,
                    Proprietaire = jouet.Proprietaire,
                    Prix = jouet.Prix,
                    listPhotos = (await _photoService.GetPhotoByIdJouet(jouet.Id)),
                    categorieJouet = categorie,
                    etatJouet = etat
                });

            });

            
            return View(toys);
        }

        // GET: ToysController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToysController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToysController/Create
        [Authorize]
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

        // GET: ToysController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ToysController/Edit/5
        [Authorize]
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

        // GET: ToysController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ToysController/Delete/5
        [Authorize]
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
