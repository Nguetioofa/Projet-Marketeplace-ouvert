using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;

namespace SiteWeb.Controllers
{
    public class JouetsController : Controller
    {
        private readonly IJouetService _jouetService;
        private readonly IPhotoService _photoService;
        private readonly IEtatJouetService _etatService;
        private readonly ICategorieJouetService _categorieJouetService;
        public JouetsController( IJouetService jouetService,IPhotoService photoService
            ,  IEtatJouetService etatJouetService
            , ICategorieJouetService categorieJouetService) 
        {
            _jouetService = jouetService;
            _photoService = photoService;
            _categorieJouetService = categorieJouetService;
            _etatService = etatJouetService;
        }

        // GET: ToysController
        public async Task<IActionResult> Index()
        {
            var jouets = await _jouetService.GetJouets();
            if (jouets is null)
            {
                return NotFound();
            }

            var etat = await _etatService.GetEtatJouets();
            var categorie = await _categorieJouetService.GetCategorieJouets();
            List<Toy> toys = new List<Toy>();

            jouets.ForEach( jouet => 
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
                    listPhotos =  _photoService.GetPhotoByIdJouet(1).Result,
                    AcceptAchat = jouet.AcceptAchat,
                    AcceptTroc = jouet.AcceptTroc,
                    categorieJouet = categorie,
                    etatJouet = etat
                }) ;

            });

            
            return View(toys);
        }

        // GET: ToysController/Details/5
     //   [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
                return NoContent();

            var jouet = await _jouetService.GetJouet(id);

            if (jouet is null)
            {
                return NotFound();
            }
            var etat = await _etatService.GetEtatJouets();
            var categorie = await _categorieJouetService.GetCategorieJouets();
            var toy = new Toy() 
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
                listPhotos = _photoService.GetPhotoByIdJouet(jouet.Id).Result,
                AcceptAchat = jouet.AcceptAchat,
                AcceptTroc = jouet.AcceptTroc,
                categorieJouet = categorie,
                etatJouet = etat
            };
            return View(toy);
        }

        // GET: ToysController/Create
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var etat = await _etatService.GetEtatJouets();
            var categorie = await _categorieJouetService.GetCategorieJouets();
            Toy toy = new Toy();
            toy.categorieJouet = categorie;
            toy.etatJouet = etat;
            return View(toy);
        }

        // POST: ToysController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Toy toy, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                var jouet = new Jouet() 
                {
                    Id = toy.Id,
                    Nom = toy.Nom,
                    Categorie = toy.Categorie,
                    AgeMin = toy.AgeMin,
                    AgeMax = toy.AgeMax,
                    EtatId = toy.EtatId,
                    Descriptions = toy.Descriptions,
                    Proprietaire = toy.Proprietaire,
                    Prix = toy.Prix,
                    AcceptAchat = toy.AcceptAchat,
                    AcceptTroc = toy.AcceptTroc,
                    
                };

                var jouetAdd = await _jouetService.AddJouet(jouet);
                if (jouetAdd is null)
                {
                    ViewBag.Erreur = "Ajout echouer";
                    return View(toy);
                }

                if (images != null && images.Count > 0)
                {
                   var succ = await _photoService.AddPhotos(toy.listPhotos,images,jouetAdd.Id);
                    if (!succ)
                    {
                        ViewBag.Erreur = "l'ajout d'une ou plusieurs photo a echouer";
                        return View(toy);
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(toy);
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
