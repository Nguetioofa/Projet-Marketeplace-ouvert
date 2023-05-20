using Microsoft.AspNetCore.Mvc;
using ChangeToyServices.Interfaces;
using ModelsLibrary.Models.Users;
using ModelsLibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SiteWeb.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateursController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _utilisateurService.GetUtilisateurs();
            return View(result.Value);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _utilisateurService.GetUtilisateur(id);
            return View(result.Value);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UtilisateurL utilisateur)
        {
            if (ModelState.IsValid)
            {
                await _utilisateurService.AddUtilisateur(utilisateur);
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateur);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _utilisateurService.GetUtilisateur(id);
            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UtilisateurL utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _utilisateurService.UpdateUtilisateur(utilisateur);
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateur);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _utilisateurService.GetUtilisateur(id);
            return View(result.Value);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _utilisateurService.DeleteUtilisateur(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserAuthen userAuthen)
        {
            if (ModelState.IsValid)
            {

                var result = (await _utilisateurService.Login(userAuthen));
                
                if (result.principal != null)
                {
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.principal);
                    ViewBag.ErrorMessage = result.principal.Claims.FirstOrDefault().Value;
                    //return RedirectToAction("Index", "Home");

                }
                else
                {
                    //var error = result as MessageErrorG;
                    ViewBag.ErrorMessage = result.errorMessage;
                }
            }

            return View(userAuthen);
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
