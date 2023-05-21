using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Users;
using ModelsLibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Principal;
using NuGet.Packaging;
using SiteWeb.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserAuthen userAuthen)
        {
            if (ModelState.IsValid)
            {

                var result = (await _utilisateurService.Login(userAuthen));
                
                if (result.useraut != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(result.useraut.Token);
                    var claimsToken = jwtToken.Claims.ToList();
                    var claimssString = claimsToken
                                                    .Where(c => c.Type == "role")
                                                    .Select(c => c.Value).ToList();

                    var NameUser = claimsToken.Where(r => r.Type == "NameLastName")
                                                      .Select(r=> r.Value).First();
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, result.useraut.Id.ToString()),
                        new Claim(ClaimTypes.Email, result.useraut.EmailId),
                        new Claim("Token", result.useraut.Token),
                        new Claim("NameLastName", NameUser),
                    };

                    claims.AddRange(claimssString.Select(str => new Claim(ClaimTypes.Role, str)));
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var authentificationPropertie = new AuthenticationProperties
                    {
                        IsPersistent = true,                       
                        ExpiresUtc = result.useraut.ExpiredTime
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authentificationPropertie);
                    var claimsPrincipal = HttpContext.User;


                  
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    //var error = result as MessageErrorG;
                    ViewBag.ErrorMessage = result.errorMessage;
                }
            }

            return View(userAuthen);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Utilisateurs");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserResisterDto userResisterDto)
        {
            if (ModelState.IsValid)
            {

                var result = (await _utilisateurService.Register(userResisterDto));

                if (result.iSucess)
                {
                   
                    return RedirectToAction( "Login");

                }
                else
                {
                    //var error = result as MessageErrorG;
                    ViewBag.ErrorMessage = result.message;
                }
            }

            return View(userResisterDto);

        }
    }
}
