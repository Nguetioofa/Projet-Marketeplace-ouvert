using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models.Echanges;

namespace SiteWeb.Services.Interfaces
{
    public interface IEchangeService
    {
        public Task<ActionResult<List<EchangeL>>> GetEchanges();
        public Task<ActionResult<EchangeL>> GetEchange(int id);
        public Task<bool> UpdateEchange(EchangeL echange);
        public Task<bool> AddEchange(EchangeL echange);
        public Task<bool> DeleteEchange(int id);

    }
}
