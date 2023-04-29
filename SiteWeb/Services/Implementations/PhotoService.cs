using Microsoft.AspNetCore.Mvc;
using SiteWeb.Data;
using SiteWeb.Services.Interfaces;
using System.Text.Json;
using System.Text;

namespace SiteWeb.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Photos";

        public PhotoService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<Photo>>> GetPhotos()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Photos = JsonSerializer.Deserialize<List<Photo>>(content);

                return Photos;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Photo = JsonSerializer.Deserialize<Photo>(content);

                return Photo;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdatePhoto(Photo Photo)
        {
            var data = JsonSerializer.Serialize(Photo);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddPhoto(Photo Photo)
        {
            var data = JsonSerializer.Serialize(Photo);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeletePhoto(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
