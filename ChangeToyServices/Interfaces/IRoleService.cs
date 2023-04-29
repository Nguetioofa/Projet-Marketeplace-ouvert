using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IRoleService
    {
        public Task<ActionResult<List<Role>>> GetRoles();
        public Task<ActionResult<Role>> GetRole(int id);
        public Task<bool> UpdateRole(Role Role);
        public Task<bool> AddRole(Role Role);
        public Task<bool> DeleteRole(int id);
    }
}
