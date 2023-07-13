using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Claims;
using ModelsLibrary.Models.Annonces;

namespace SiteWeb.Controllers
{
    public class AnnoncesController : Controller
	{
		private readonly IAnnonceService _annonceService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IPhotoService _photoService;
		private readonly IUtilisateurService _utilisateurService;
		public AnnoncesController(IAnnonceService annonceService, IWebHostEnvironment webHostEnvironment,
				IPhotoService photoService, IUtilisateurService utilisateurService)
		{
			_annonceService = annonceService;
			_webHostEnvironment = webHostEnvironment;
			_photoService = photoService;
			_utilisateurService = utilisateurService;
		}

		// GET: AnnoncesController
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var listAnnonces = await _annonceService.GetAnnonces();

			List<AnnoncesGridModel> annoncesGridModel = new List<AnnoncesGridModel>();

			if (listAnnonces is null)
			{
				return NotFound();
			}
			foreach (var annonce in listAnnonces)
			{
				var photo = (await _photoService.GetPhotoByIdAnnonce((int)annonce.Id)).Where(ph => !ph.UrlP.Contains("400x400")).FirstOrDefault();
				var utilisateur = (await _utilisateurService.GetUtilisateur((int)annonce.IdUtilisateur));
				if (utilisateur is null)
				{
					return NotFound();
				}
				//throw new Exception("Une erreur s'est produit");

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

		// GET: AnnoncesController
		[HttpGet]
		public async Task<IActionResult> AnnoncesUtilisateur(int id)
		{
			var listAnnonces = await _annonceService.GetAnnonceByIdUtilisateur(id);

			List<AnnoncesGridModel> annoncesGridModel = new List<AnnoncesGridModel>();

			if (listAnnonces is null)
			{
				return BadRequest();
			}
			foreach (var annonce in listAnnonces)
			{
				var photo = (await _photoService.GetPhotoByIdAnnonce((int)annonce.Id)).Where(ph => !ph.UrlP.Contains("400x400")).FirstOrDefault();
				var utilisateur = (await _utilisateurService.GetUtilisateur((int)annonce.IdUtilisateur));
				if (utilisateur is null)
				{
					return BadRequest();
				}
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
			return Ok(annoncesGridModel);

		}

		// GET: AnnoncesController/Details/5
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (id == 0)
				return NotFound();

			var annonce = await _annonceService.GetAnnonce(id);


			if (annonce is null)
			{
				return NotFound();

			}
			var photos = (await _photoService.GetPhotoByIdAnnonce((int)annonce.Id)).Where(ph => !ph.UrlP.Contains("400x400")).ToList();
			var utilisateur = (await _utilisateurService.GetUtilisateur((int)annonce.IdUtilisateur));
			if (utilisateur is null)
			{
				ViewBag.ErrorMessage = "Une erreur s'est produit";
				return NotFound();
			}

			AnnoncesDetailModel annoncesDetailModel = new AnnoncesDetailModel()
			{
				Id = (int)annonce.Id,
				IdUtilisateur = utilisateur.Id,
				utilisateur = utilisateur,
				Titre = annonce.Titre,
				DescriptionAnnonce = annonce.DescriptionAnnonce,
				listPhoto = photos,
				DateAnnonce = annonce.DateAnnonce,
			};

			return View(annoncesDetailModel);


		}

		// GET: AnnoncesController/Create
		[Authorize]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		// POST: AnnoncesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] AnnonceL annonce, [FromForm] List<IFormFile> images)
		{
			try
			{
				if (ModelState.IsValid)
				{
					int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

					annonce.IdUtilisateur = idUser;
					annonce.Id = 0;
					annonce.DateAnnonce = DateTime.UtcNow;

					var annonceAdd = await _annonceService.AddAnnonce(annonce);
					if (annonceAdd is null)
					{
						ViewBag.ErrorMessage = "Ajout de l'annonce echouer";
						annonce.IdUtilisateur = null;
						return View(annonce);
					}
					int count1 = 0;
					bool isFrist = true;
					if (images != null && images.Count > 0)
					{
						foreach (var photo in images)
						{

							if (photo.Length > 0)
							{
								var fileName = Path.GetFileNameWithoutExtension(photo.FileName).Replace(" ", "-");

								fileName += "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

								fileName += Path.GetExtension(photo.FileName);

								var photoL = new PhotoL
								{
									NomPhoto = fileName,
									DescriptionPhoto = "",
									DatePublication = DateTime.UtcNow,
									Annonce = annonce.Id
								};
								var year = DateTime.Now.Year.ToString();
								var month = DateTime.Now.ToString("MMMM");
								var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", year, month, "600x600", "Annonces", fileName);
								var relatifPath = (Path.Combine("images", year, month, "600x600", "Annonces", fileName)).Replace("\\", "/");
								var directoryPath = Path.GetDirectoryName(imagePath);

								if (!Directory.Exists(directoryPath))
								{
									Directory.CreateDirectory(directoryPath);
								}
								using (var memoryStream = new MemoryStream())
								{
									await photo.CopyToAsync(memoryStream);
									using (var image = new Bitmap(memoryStream))
									{
										var resizedImage = new Bitmap(image, new System.Drawing.Size(600, 600));

										// Enregistrer l'image redimensionnée au format JPEG avec un niveau de qualité spécifié
										var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
										var encoderParams = new EncoderParameters(1);
										encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 75L);
										resizedImage.Save(imagePath, encoder, encoderParams);

										photoL.Taille = (int)new FileInfo(imagePath).Length;
										photoL.Format = photo.ContentType;
										photoL.UrlP = $"/{relatifPath}";
										photoL.Jouet = null;
										photoL.Profil = null;
										photoL.Messages = null;
										photoL.Annonce = annonceAdd.Id;

										if (isFrist)
										{
											var thumbnailImage = new Bitmap(image, new System.Drawing.Size(400, 400));

											// Enregistrer la deuxième image redimensionnée dans le dossier souhaité
											var thumbnailPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", year, month, "400x400", "Annonces", fileName);
											var directorythumbnailPath = Path.GetDirectoryName(thumbnailPath);

											if (!Directory.Exists(directorythumbnailPath))
											{
												Directory.CreateDirectory(directorythumbnailPath);
											}
											// Enregistrer l'image redimensionnée au format JPEG avec un niveau de qualité spécifié
											encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
											encoderParams = new EncoderParameters(1);
											encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 75L);

											thumbnailImage.Save(thumbnailPath, encoder, encoderParams);
											var relatifthumbnailPath = (Path.Combine("images", year, month, "400x400", "Annonces", fileName)).Replace("\\", "/");

											isFrist = false;
											var photoL2 = new PhotoL
											{
												NomPhoto = fileName,
												DescriptionPhoto = "",
												DatePublication = DateTime.UtcNow,
												Annonce = annonceAdd.Id,
												Taille = (int)new FileInfo(imagePath).Length,
												Format = photo.ContentType,
												UrlP = $"/{relatifthumbnailPath}",
												Jouet = null,
												Profil = null,
												Messages = null
											};

											var isSuccess1 = await _photoService.AddPhoto(photoL2);

											if (!isSuccess1)
											{
												count1++;
											}
										}

									}
								}

								var isSuccess = await _photoService.AddPhoto(photoL);
								if (!isSuccess)
								{
									count1++;
								}
							}
						}
						if (count1 > 0)
						{
							ViewBag.ErrorMessage = $"l'ajout de {count1} photo a echouer";
							return View(annonce);
						}
					}
					return RedirectToAction(nameof(Index));
				}
				return RedirectToAction(nameof(Index));
			}
			catch//(Exception ex)
			{
				ViewBag.ErrorMessage = $"Une erreur s'est produit";
				return View();
			}
		}

	}
}
