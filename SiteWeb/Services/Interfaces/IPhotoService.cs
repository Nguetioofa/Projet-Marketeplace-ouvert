using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IPhotoService
    {
        public Task<List<PhotoL>> GetPhotos();
        public Task<PhotoL> GetPhoto(int id);
        public Task<bool> UpdatePhoto(PhotoL Photo);
        public Task<bool> AddPhoto(PhotoL Photo);
        public Task<bool> AddPhotos(List<PhotoL> photos, List<IFormFile> images,int idjouet);
        public Task<bool> DeletePhoto(int id);
        public Task<List<PhotoL>> GetPhotoByIdJouet(int id);
		public Task<List<PhotoL>> GetPhotoByIdAnnonce(int id);
		public Task<List<PhotoL>> GetPhotoByIdProfil(int id);
		public Task<List<PhotoL>> GetPhotoByIdMessage(int id);

	}
}
