using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using ModelsLibrary.Models.Toys;
using ModelsLibrary.Models.Users;

namespace SiteWeb.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Messages";

        public MessageService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<MessageL>>> GetMessages()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Messages = JsonSerializer.Deserialize<List<MessageL>>(content);

                return Messages;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<MessageL>> GetMessage(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Message = JsonSerializer.Deserialize<MessageL>(content);

                return Message;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateMessage(MessageL Message)
        {
            var data = JsonSerializer.Serialize(Message);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddMessage(MessageL Message)
        {
            var data = JsonSerializer.Serialize(Message);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteMessage(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }

		public async Task<List<MessageL>> GetMessageByIdUtilisateur(int id)
		{
			var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetMessageByIdUtilisateur/{id}");
			if (response.IsSuccessStatusCode)
			{
				var messages = await response.Content.ReadFromJsonAsync<List<MessageL>>();
				return messages;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<MessageL>> GetMessageByConversation(int idUser1, int idUser2)
		{
			var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetMessageByConversation/{idUser2}/{idUser1}");
			if (response.IsSuccessStatusCode)
			{
				var messages = await response.Content.ReadFromJsonAsync<List<MessageL>>();
				return messages;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<UserIdName>> GetAllConversationByIdUtilisateur(int id)
		{
			var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetAllConversationByIdUtilisateur/{id}");
			if (response.IsSuccessStatusCode)
			{
				var userIdNames = await response.Content.ReadFromJsonAsync<List<UserIdName>>();
				return userIdNames;
			}
			else
			{
				return null;
			}
		}
	}
}
