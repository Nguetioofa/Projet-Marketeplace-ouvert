using Microsoft.AspNetCore.SignalR;
using SiteWeb.Services.Interfaces;

namespace SiteWeb.Hubs
{
	public class ChatHub : Hub
	{
		private readonly IMessageService _messageService;

		public ChatHub(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public async Task SendMessage(int idExpediteur, int idDestinataire, string contenu)
		{
			var message = new MessageL
			{
				Id = 0,
				Contenu = contenu,
				DateM = DateTime.Now,
				IdExpediteur = idExpediteur,
				IdDestinataire = idDestinataire
			};

			await _messageService.AddMessage(message);

			await Clients.All.SendAsync("ReceiveMessage", idExpediteur, idDestinataire, contenu, DateTime.Now);
		}

		public async Task GetConversationMessages(int idUser1, int idUser2)
		{
			var messages = await _messageService.GetMessageByConversation(idUser1, idUser2);

			await Clients.Caller.SendAsync("ReceiveConversationMessages", messages);
		}

	}
}
