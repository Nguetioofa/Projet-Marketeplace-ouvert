using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Security.Claims;


namespace SiteWeb.Controllers
{
    public class JouetsController : Controller
    {
        private readonly IJouetService _jouetService;
        private readonly IPhotoService _photoService;
        private readonly IEtatJouetService _etatService;
        private readonly ICategorieJouetService _categorieJouetService;
        private readonly IWebHostEnvironment _environment;

        public JouetsController(IWebHostEnvironment environment, IJouetService jouetService,IPhotoService photoService
            ,  IEtatJouetService etatJouetService
            , ICategorieJouetService categorieJouetService) 
        {
            _environment = environment;
            _jouetService = jouetService;
            _photoService = photoService;
            _categorieJouetService = categorieJouetService;
            _etatService = etatJouetService;
        }

        // GET: ToysController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var jouets = await _jouetService.GetJouets();
            if (jouets is null)
            {
                return NotFound();
            }

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
                    listPhotos =  _photoService.GetPhotoByIdJouet(jouet.Id).Result,
                    AcceptAchat = jouet.AcceptAchat,
                    AcceptTroc = jouet.AcceptTroc,

                }) ;

            });
            
            return View(toys);
        }

        // GET: ToysController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
                return NoContent();

            var jouet = await _jouetService.GetJouet(id);
            if (jouet is null)
            {
                return NotFound();
            }

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
                //listPhotos = _photoService.GetPhotoByIdJouet(jouet.Id).Result,
                AcceptAchat = jouet.AcceptAchat,
                AcceptTroc = jouet.AcceptTroc,
            };
            return View(toy);
        }

        // GET: ToysController/Create
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToysController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Toy toy, [FromForm] List<IFormFile> images)
        {
			try
			{
				if (ModelState.IsValid)
				{
					int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

					var jouet = new Jouet()
					{
						Id = toy.Id,
						Nom = toy.Nom,
						Categorie = toy.Categorie,
						AgeMin = toy.AgeMin,
						AgeMax = toy.AgeMax,
						EtatId = toy.EtatId,
						Descriptions = toy.Descriptions,
						Proprietaire = idUser,
						Prix = toy.Prix,
						AcceptAchat = toy.AcceptAchat,
						AcceptTroc = toy.AcceptTroc,
						EstPublier = toy.EstPublier

					};

					var jouetAdd = await _jouetService.AddJouet(jouet);
					if (jouetAdd is null)
					{
						ViewBag.ErrorMessage = "Ajout du jouet echouer";
						return View(toy);
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
									Jouet = toy.Id
								};
								//DateTime dateTime = DateTime.UtcNow;
								//string dateString = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
								//string dateString = "2023-07-05T05:24:06.313Z";
								///////////////////////////////////////////////
								//DateTime dateTime;
								//if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
								//{
								//    // La chaîne a été convertie avec succès en objet DateTime
								//}
								//else
								//{
								//    // La chaîne n'est pas au format attendu
								//}

								//var imagePath = Path.Combine(_environment.WebRootPath, "images", DateTime.Now.ToString("yyyyMM"), fileName);
								//var directoryPath = Path.GetDirectoryName(imagePath);

								var year = DateTime.Now.Year.ToString();
								var month = DateTime.Now.ToString("MMMM");
								var imagePath = Path.Combine(_environment.WebRootPath, "images", "Toys", year, month, "600x600", fileName);
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
											var thumbnailPath = Path.Combine(_environment.WebRootPath, "images", "Toys", year, month, "400x400", fileName);
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
							return View(toy);
						}
					}
					return RedirectToAction(nameof(Index));
				}

				return View(toy);

				//return RedirectToAction(nameof(Index));
			}
			catch//(Exception ex)
			{
				ViewBag.ErrorMessage = $"Une erreur s'est produit";
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
