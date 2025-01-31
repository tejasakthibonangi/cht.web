using Cht.HMS.Web.UI.Models;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();

        Task<Role> InsertOrUpdateRoleAsync(Role role);

        Task<Role> GetRoleByIdAsync(Guid roleId);

    }
}
