using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IPhotoService
    {
        public Task<ActionResult<List<Photo>>> GetPhotos();
        public Task<ActionResult<Photo>> GetPhoto(int id);
        public Task<bool> UpdatePhoto(Photo Photo);
        public Task<bool> AddPhoto(Photo Photo);
        public Task<bool> DeletePhoto(int id);
    }
}
