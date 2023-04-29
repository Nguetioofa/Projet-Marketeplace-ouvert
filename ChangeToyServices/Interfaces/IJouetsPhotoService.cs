using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IJouetsPhotoService
    {
        public Task<ActionResult<List<JouetsPhoto>>> GetJouetsPhotos();
        public Task<ActionResult<JouetsPhoto>> GetJouetsPhoto(int id);
        public Task<bool> UpdateJouetsPhoto(JouetsPhoto JouetsPhoto);
        public Task<bool> AddJouetsPhoto(JouetsPhoto JouetsPhoto);
        public Task<bool> DeleteJouetsPhoto(int id);
    }
}
