using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Interfaces;

namespace SiteWeb.Controllers
{
	public class RechercheController : Controller
	{
		private readonly IJouetService _jouetService;
		private readonly IPhotoService _photoService;

		public RechercheController(IJouetService jouetService, IPhotoService photoService)
		{
			_jouetService = jouetService;
			_photoService = photoService;
		}

		[HttpGet]
		// GET: RechecheController
		public async Task<IActionResult> Index(string name)
		{
			var jouets = await _jouetService.GetJouetsByName(name);
			List<ToyBoxModel> toyBoxModels = new List<ToyBoxModel>();

			if (jouets is not null)
			{
				foreach (var jouet in jouets)
				{
					var photo = (await _photoService.GetPhotoByIdJouet(jouet.Id)).Where(ph => ph.UrlP.Contains("400x400")).FirstOrDefault();
					// if (photo is not null)
					// {
					//     photo.UrlP = photo.UrlP.Replace("600x600", "600x600");

					// }

					toyBoxModels.Add(new ToyBoxModel()
					{
						Id = jouet.Id,
						Nom = jouet.Nom,
						Categorie = jouet.Categorie,
						Prix = jouet.Prix,
						Photo = photo,
						AcceptAchat = jouet.AcceptAchat,
						AcceptTroc = jouet.AcceptTroc,
					});
				}
			}

			return View(toyBoxModels);
		}

	}
}
