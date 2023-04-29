using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IMessageService
    {
        public Task<ActionResult<List<Message>>> GetMessages();
        public Task<ActionResult<Message>> GetMessage(int id);
        public Task<bool> UpdateMessage(Message Message);
        public Task<bool> AddMessage(Message Message);
        public Task<bool> DeleteMessage(int id);
    }
}
