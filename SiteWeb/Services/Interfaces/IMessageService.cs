using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;
using ModelsLibrary.Models.Users;

namespace SiteWeb.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ActionResult<List<MessageL>>> GetMessages();
        public Task<ActionResult<MessageL>> GetMessage(int id);
        public Task<bool> UpdateMessage(MessageL Message);
        public Task<bool> AddMessage(MessageL Message);
        public Task<bool> DeleteMessage(int id);
		public Task<List<MessageL>> GetMessageByIdUtilisateur(int id);
		public Task<List<MessageL>> GetMessageByConversation(int idUser1,int idUser2);
		public Task<List<UserIdName>> GetAllConversationByIdUtilisateur(int id);

	}
}
