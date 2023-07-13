using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Annonces;

namespace SiteWeb.Services.Interfaces
{
    public interface IAnnonceService
    {
        public Task<List<AnnonceL>> GetAnnonces();
        public Task<AnnonceL> GetAnnonce(int id);
        public Task<bool> UpdateAnnonce(AnnonceL annonce);
        public Task<AnnonceL> AddAnnonce(AnnonceL annonce);
        public Task<bool> DeleteAnnonce(int id);
		public Task<List<AnnonceL>> GetAnnonceByIdUtilisateur(int id);
        public Task<List<AnnonceL>> LastAnnonces(int id);

    }
}
