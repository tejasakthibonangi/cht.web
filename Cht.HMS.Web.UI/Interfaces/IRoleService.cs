using Cht.HMS.Web.UI.Models;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IRoleService
    {
        Task<Role> InsertOrUpdateRole(Role role);
        Task<Role> GetRoleByIdAsync(Guid roleId);
        Task<List<Role>> GetRolesAsync();
    }
}
