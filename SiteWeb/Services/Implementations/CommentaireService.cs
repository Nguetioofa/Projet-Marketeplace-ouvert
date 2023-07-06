using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class CommentaireService : ICommentaireService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Commentaires";

        public CommentaireService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<CommentaireL>> GetCommentaires()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var commentaires = await response.Content.ReadFromJsonAsync<List<CommentaireL>>();

                return commentaires;

            }
            else
            {
				return null;
			}

		}

        public async Task<ActionResult<CommentaireL>> GetCommentaire(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var commentaire = await response.Content.ReadFromJsonAsync<CommentaireL>();

                return commentaire;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateCommentaire(CommentaireL commentaire)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), commentaire);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddCommentaire(CommentaireL commentaire)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), commentaire);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteCommentaire(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }

		public async Task<List<CommentaireL>> GetCommentaireByIdJouet(int id)
		{
			var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetCommentaireByIdJouet/{id}");
			if (response.IsSuccessStatusCode)
			{
				var Commentaires = await response.Content.ReadFromJsonAsync<List<CommentaireL>>();
				return Commentaires;
			}
			else
			{
				return null;
			}
		}
	}
}
