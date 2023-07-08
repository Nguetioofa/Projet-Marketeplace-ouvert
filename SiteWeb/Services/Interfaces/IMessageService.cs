using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ActionResult<List<MessageL>>> GetMessages();
        public Task<ActionResult<MessageL>> GetMessage(int id);
        public Task<bool> UpdateMessage(MessageL Message);
        public Task<bool> AddMessage(MessageL Message);
        public Task<bool> DeleteMessage(int id);
    }
}
