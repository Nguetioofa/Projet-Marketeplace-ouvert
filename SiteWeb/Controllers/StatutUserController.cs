using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using SiteWeb.Services.Interfaces;

namespace SiteWeb.Controllers
{
    public class StatutUserController : Controller
    {
        private readonly IStatutUserService _statutUserService;

        public StatutUserController(IStatutUserService statutUserService)
        {
            _statutUserService = statutUserService;
        }

        [Authorize(Policy = "AdministrateurSeulement")]
      //  [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _statutUserService.GetStatutUsers();
                return View(result.Value);
            }
            return RedirectToAction("Index", "Home");

        }

        [Authorize(Policy = "AdministrateurSeulement")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _statutUserService.GetStatutUser(id);
            return View(result.Value);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatutUserL statutUser)
        {
            if (ModelState.IsValid)
            {
                await _statutUserService.AddStatutUser(statutUser);
                return RedirectToAction(nameof(Index));
            }
            return View(statutUser);
        }
        [Authorize(Policy = "AdministrateurSeulement")]

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _statutUserService.GetStatutUser(id);
            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StatutUserL statutUser)
        {
            if (id != statutUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _statutUserService.UpdateStatutUser(statutUser);
                return RedirectToAction(nameof(Index));
            }
            return View(statutUser);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _statutUserService.GetStatutUser(id);
            return View(result.Value);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _statutUserService.DeleteStatutUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
