using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Echanges;

namespace SiteWeb.Services.Interfaces
{
    public interface IEchangeService
    {
        public Task<List<EchangeL>> GetEchanges();
        public Task<EchangeL> GetEchange(int id);
        public Task<List<EchangeL>> GetEchangesByIdUser(int id);
        public Task<bool> UpdateEchange(EchangeL echange);
        public Task<bool> ChangeStatutTransaction(int idechange, int idstatut);

        public Task<bool> AddEchange(EchangeL echange);
        public Task<bool> DeleteEchange(int id);

    }
}
