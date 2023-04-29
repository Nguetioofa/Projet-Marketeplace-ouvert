using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IEchangeService
    {
        public Task<ActionResult<List<Echange>>> GetEchanges();
        public Task<ActionResult<Echange>> GetEchange(int id);
        public Task<bool> UpdateEchange(Echange echange);
        public Task<bool> AddEchange(Echange echange);
        public Task<bool> DeleteEchange(int id);

    }
}
