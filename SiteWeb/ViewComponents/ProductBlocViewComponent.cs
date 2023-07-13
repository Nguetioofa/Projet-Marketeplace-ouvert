using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Interfaces;
using System.Xml.Linq;

namespace SiteWeb.ViewComponents
{
	[ViewComponent]
	public class ProductBlocViewComponent : ViewComponent
	{
		private readonly IPhotoService _photoService;
		private readonly IJouetService _jouetService;

		public ProductBlocViewComponent(IJouetService jouetService,IPhotoService photoService)
		{
			_jouetService = jouetService;
			_photoService = photoService;
		}

		public async Task<IViewComponentResult> InvokeAsync(string name)
		{
			var jouets = await _jouetService.GetJouetsByNameCategorie(name);
			List<ToyBoxModel> toyBoxModels = new List<ToyBoxModel>();

			if (jouets is not null)
			{
				foreach (var jouet in jouets)
				{
					var photo = (await _photoService.GetPhotoByIdJouet(jouet.Id)).Where(ph => ph.UrlP.Contains("400x400")).FirstOrDefault();

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
