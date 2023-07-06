using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Claims;

namespace SiteWeb.Controllers
{
	public class AnnoncesController : Controller
	{
		private readonly IAnnonceService _annonceService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IPhotoService _photoService;
		public AnnoncesController(IAnnonceService annonceService, IWebHostEnvironment webHostEnvironment,
				IPhotoService photoService) 
		{
			_annonceService = annonceService;
			_webHostEnvironment = webHostEnvironment;
			_photoService = photoService;
		}

		// GET: AnnoncesController
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var listAnnonces = await _annonceService.GetAnnonces();
			return View();
		}

		// GET: AnnoncesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
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
					annonce.DateAnnonce = DateTime.UtcNow;

					var annonceAdd = await _annonceService.AddAnnonce(annonce);
					if (annonceAdd is null)
					{
						ViewBag.ErrorMessage = "Ajout de l'annonce echouer";
						annonce.IdUtilisateur = null;
						return View(annonce);
					}
					int count = 0;
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
									 = toy.Id
								};
								var year = DateTime.Now.Year.ToString();
								var month = DateTime.Now.ToString("MMMM");
								var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Toys", year, month, "600x600", fileName);
								var relatifPath = (Path.Combine("images", "Toys", year, month, "600x600", fileName)).Replace("\\", "/");
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
										var resizedImage = new Bitmap(image, new Size(600, 600));

										// Enregistrer l'image redimensionnée au format JPEG avec un niveau de qualité spécifié
										var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
										var encoderParams = new EncoderParameters(1);
										encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 75L);
										resizedImage.Save(imagePath, encoder, encoderParams);

										photoL.Taille = (int)new FileInfo(imagePath).Length;
										photoL.Format = photo.ContentType;
										photoL.UrlP = $"/{relatifPath}";
										photoL.Jouet = jouetAdd.Id;
										photoL.Profil = null;
										photoL.Messages = null;

										if (isFrist)
										{
											// Créer une deuxième image redimensionnée en format 100x100 pixels
											var thumbnailImage = new Bitmap(image, new Size(400, 400));

											// Enregistrer la deuxième image redimensionnée dans le dossier souhaité
											var thumbnailPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Toys", year, month, "400x400", fileName);
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
											isFrist = false;
											var photoL2 = new PhotoL
											{
												NomPhoto = fileName,
												DescriptionPhoto = "",
												DatePublication = DateTime.UtcNow,
												Jouet = toy.Id
											};
											var relatifthumbnailPath = (Path.Combine("images", "Toys", year, month, "400x400", fileName)).Replace("\\", "/");

											photoL2.Taille = (int)new FileInfo(imagePath).Length;
											photoL2.Format = photo.ContentType;
											photoL2.UrlP = $"/{relatifthumbnailPath}";
											photoL2.Jouet = jouetAdd.Id;
											photoL2.Profil = null;
											photoL2.Messages = null;
											if (!(await _photoService.AddPhoto(photoL2)))
											{
												count++;
											}
										}

									}
								}

								var isSuccess = await _photoService.AddPhoto(photoL);
								if (!isSuccess)
								{
									count++;
								}
							}
						}
						if (count > 0)
						{
							ViewBag.ErrorMessage = $"l'ajout de {count} photo a echouer";
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

		// GET: AnnoncesController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: AnnoncesController/Edit/5
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

		// GET: AnnoncesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: AnnoncesController/Delete/5
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
