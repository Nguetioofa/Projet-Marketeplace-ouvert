using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IStatutUserService
    {
        public Task<ActionResult<IEnumerable<StatutUser>>> GetStatutUsers();
        public Task<ActionResult<StatutUser>> GetStatutUser(int id);
        public Task<bool> UpdateStatutUser(StatutUser StatutUser);
        public Task<bool> AddStatutUser(StatutUser StatutUser);
        public Task<bool> DeleteStatutUser(int id);
    }
}
