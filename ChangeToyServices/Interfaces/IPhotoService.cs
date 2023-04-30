using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IPhotoService
    {
        public Task<ActionResult<List<PhotoL>>> GetPhotos();
        public Task<ActionResult<PhotoL>> GetPhoto(int id);
        public Task<bool> UpdatePhoto(PhotoL Photo);
        public Task<bool> AddPhoto(PhotoL Photo);
        public Task<bool> DeletePhoto(int id);
    }
}
