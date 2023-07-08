using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Toys;

namespace SiteWeb.Services.Interfaces
{
    public interface IJouetService
    {
        public Task<List<JouetL>> GetJouets();
		public Task<List<JouetL>> GetJouetsByIdCategorie(int id);
		public Task<List<JouetL>> GetJouetsByNameCategorie(string name);

		public Task<JouetL> GetJouet(int id);
        public Task<bool> UpdateJouet(JouetL Jouet);
        public Task<JouetL> AddJouet(JouetL Jouet);
        public Task<bool> DeleteJouet(int id);
    }
}
