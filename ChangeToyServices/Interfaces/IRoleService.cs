using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IRoleService
    {
        public Task<ActionResult<List<Rolel>>> GetRoles();
        public Task<ActionResult<Rolel>> GetRole(int id);
        public Task<bool> UpdateRole(Rolel Role);
        public Task<bool> AddRole(Rolel Role);
        public Task<bool> DeleteRole(int id);
    }
}
