using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Annonces;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;

namespace SiteWeb.Components
{
   [ViewComponent]
    public class DernieresAnnoncesViewComponent : ViewComponent
    {
        private readonly IAnnonceService _annonceService;
		private readonly IPhotoService _photoService;
		private readonly IUtilisateurService _utilisateurService;

        public DernieresAnnoncesViewComponent(IAnnonceService annonceService,IUtilisateurService utilisateurService
												,IPhotoService photoService)
        {
            _annonceService = annonceService;
			_photoService = photoService;
			_utilisateurService = utilisateurService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
			List<AnnoncesGridModel> annoncesGridModel = new List<AnnoncesGridModel>();

			var annonces = await _annonceService.LastAnnonces(4);
			foreach (var annonce in annonces)
			{
				var photo = (await _photoService.GetPhotoByIdAnnonce((int)annonce.Id)).Where(ph => !ph.UrlP.Contains("400x400")).FirstOrDefault();
				var utilisateur = (await _utilisateurService.GetUtilisateur((int)annonce.IdUtilisateur));

				annoncesGridModel.Add(new AnnoncesGridModel()
				{
					Id = (int)annonce.Id,
					IdUtilisateur = utilisateur.Id,
					utilisateur = utilisateur,
					Titre = annonce.Titre,
					DescriptionAnnonce = annonce.DescriptionAnnonce,
					Photo = photo,
					DateAnnonce = annonce.DateAnnonce,
				});
			}
			return View(annoncesGridModel);
        }
    }
}
