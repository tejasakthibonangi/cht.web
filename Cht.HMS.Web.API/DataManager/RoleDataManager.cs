using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class RoleDataManager : IRoleManager
    {
        private readonly ApplicationDBContext _dbContext;
        public RoleDataManager(ApplicationDBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            return await _dbContext.roles.FindAsync(roleId);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _dbContext.roles.ToListAsync();
        }

        public async Task<Role> InsertOrUpdateRole(Role role)
        {
            if(role != null)
            {
                if(role.RoleId == Guid.Empty)
                {
                    _dbContext.roles.AddAsync(role);
                }
                else
                {
                    var _role = await _dbContext.roles.FindAsync(role.RoleId);

                    if(_role != null)
                    {
                        _role.Name = role.Name;
                        _role.Code = role.Code;
                        _role.IsActive = role.IsActive;
                        _role.ModifiedBy = role.ModifiedBy;
                        _role.ModifiedOn = role.ModifiedOn;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return role;

        }
    }
}
