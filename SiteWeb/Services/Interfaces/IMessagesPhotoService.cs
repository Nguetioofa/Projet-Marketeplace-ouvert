using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IMessagesPhotoService
    {
        public Task<ActionResult<List<MessagesPhoto>>> GetMessagesPhotos();
        public Task<ActionResult<MessagesPhoto>> GetMessagesPhoto(int id);
        public Task<bool> UpdateMessagesPhoto(MessagesPhoto MessagesPhoto);
        public Task<bool> AddMessagesPhoto(MessagesPhoto MessagesPhoto);
        public Task<bool> DeleteMessagesPhoto(int id);
    }
}
