using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using ModelsLibrary.Models.Toys;

namespace SiteWeb.Services.Implementations
{
    public class JouetService : IJouetService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Jouets";

        public JouetService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<JouetL>> GetJouets()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var Jouets = await response.Content.ReadFromJsonAsync<List<JouetL>>();

                return Jouets;

            }
            else
            {
                return null;
            }

        }

        public async Task<JouetL> GetJouet(int id)
        {
            var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jouet = await response.Content.ReadFromJsonAsync<JouetL>();
                
                return jouet;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateJouet(JouetL Jouet)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Jouet);

            return response.IsSuccessStatusCode;

        }
        public async Task<JouetL> AddJouet(JouetL Jouet)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Jouet);

            if (response.IsSuccessStatusCode)
            {
               JouetL? jouet = await response.Content.ReadFromJsonAsync<JouetL>(); 
                return jouet;
            }
            return null;
            
        }
        public async Task<bool> DeleteJouet(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }

		public async Task<List<JouetL>> GetJouetsByIdCategorie(int id)
		{
				var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetJouetsByIdCategorie/{id}");
				if (response.IsSuccessStatusCode)
				{
					var Jouets = await response.Content.ReadFromJsonAsync<List<JouetL>>();
					return Jouets;
				}
				else
				{
					return new();
				}			
		}

		public async Task<List<JouetL>> GetJouetsByNameCategorie(string name)
		{
			var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetJouetsByNameCategorie/{name}");
			if (response.IsSuccessStatusCode)
			{
				var Jouets = await response.Content.ReadFromJsonAsync<List<JouetL>>();
				return Jouets;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<JouetL>> GetJoutsByIdUtilisateur(int id)
		{
			var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetJoutsByIdUtilisateur/{id}");
			if (response.IsSuccessStatusCode)
			{
				var Jouets = await response.Content.ReadFromJsonAsync<List<JouetL>>();
				return Jouets;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<JouetL>> GetJouetsByName(string name)
		{
			var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetJouetsByName/{name}");
			if (response.IsSuccessStatusCode)
			{
				var Jouets = await response.Content.ReadFromJsonAsync<List<JouetL>>();
				return Jouets;
			}
			else
			{
				return null;
			}
		}
	}
}
