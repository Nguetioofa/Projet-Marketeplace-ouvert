using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{

    public class PhotoService : IPhotoService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Photos";

        public PhotoService(IWebHostEnvironment webHostEnvironment, IConfigurationService configuration, HttpClient client)
        {
            _environment = webHostEnvironment;
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<PhotoL>> GetPhotos()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var Photos = await response.Content.ReadFromJsonAsync<List<PhotoL>>();

                return Photos;

            }
            else
            {
                return null;
            }

        }

        public async Task<PhotoL> GetPhoto(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var Photo = await response.Content.ReadFromJsonAsync<PhotoL>();

                return Photo;

            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdatePhoto(PhotoL Photo)
        {
            var response = await _client.PutAsJsonAsync($"{_configuration.ApiUrl}/{ControllerName}",Photo);
            
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddPhoto(PhotoL Photo)
        {
            var response = await _client.PostAsJsonAsync($"{_configuration.ApiUrl}/{ControllerName}", Photo);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> AddPhotos(List<PhotoL> photos, List<IFormFile> images, int idjouet)
        {
            var isSuccess = true;
            for (int i = 0; i < images.Count; i++)
            {
                var image = images[i];
                var photo = photos[i];

                var filePath = Path.Combine(_environment.WebRootPath, "images", image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
           
                // Mettre à jour les propriétés de l'objet photo
                photo.UrlP = $"/images/{image.FileName}";
               // photo.Taille = (int)image.Length;
                photo.Format = image.ContentType;
                photo.DatePublication = DateTime.Now;
                photo.Jouet = idjouet;
                var response = await _client.PostAsJsonAsync($"{_configuration.ApiUrl}/{ControllerName}", photos);
                if (!response.IsSuccessStatusCode)
                    isSuccess = false;
            }

            return isSuccess;

        }

        public async Task<bool> DeletePhoto(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }

        public async Task<List<PhotoL>> GetPhotoByIdJouet(int id)
        {
            //var response = await _client.GetAsync($"http://localhost:5219/api/Photos/GetPhotoByIdJouet/{id}");
            var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetPhotoByIdJouet/{id}");
            if (response.IsSuccessStatusCode)
            {
                var Photos = await response.Content.ReadFromJsonAsync<List<PhotoL>>();
                return Photos;
            }
            else
            {
                return null;
            }
        }
    }
}
