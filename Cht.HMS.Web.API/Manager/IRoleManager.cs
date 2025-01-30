using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IRoleManager
    {
        Task<Role> InsertOrUpdateRole(Role role);
        Task<Role> GetRoleByIdAsync(Guid roleId);
        Task<List<Role>> GetRolesAsync();
    }
}
