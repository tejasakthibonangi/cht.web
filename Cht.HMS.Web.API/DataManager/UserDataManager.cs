using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class UserDataManager : IUserManager
    {
        private readonly ApplicationDBContext _dBContext;
        public UserDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<IEnumerable<UserInfirmation>> FetchUsersAsync()
        {
            List<UserInfirmation> userInfirmations = new List<UserInfirmation>();

            userInfirmations = (from user in _dBContext.users.ToList()
                                join role in _dBContext.roles.ToList() on user.RoleId equals role.Id into roleJoinInfo   from roleInfo in roleJoinInfo.DefaultIfEmpty()
                                select new UserInfirmation
                                {
                                    Id = user.Id,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    FullName = user.FirstName + " " + user.LastName,
                                    Email = user.Email,
                                    Phone = user.Phone,
                                    RoleId = user.RoleId,
                                    RoleName = roleInfo?.Name,
                                    IsBlocked = user.IsBlocked,
                                    LastPasswordChangedOn = user.LastPasswordChangedOn,
                                    CreatedBy = user.CreatedBy,
                                    CreatedOn = user.CreatedOn,
                                    ModifiedBy = user.ModifiedBy,
                                    ModifiedOn = user.ModifiedOn,
                                    IsActive = user.IsActive
                                }).ToList();

            return userInfirmations;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync(string email)
        {
            var response = await _dBContext.users.Where(x => x.Email == email).FirstOrDefaultAsync();

            if (response != null)
            {
                var userRole = await _dBContext.roles.FindAsync(response.RoleId);

                return new ApplicationUser { Id = response.Id, FirstName = response.FirstName, LastName = response.LastName, Email = response.Email, RoleId = response.RoleId, RoleName = userRole.Name, FullName = response.FirstName + " " + response.LastName };
            }

            return null;
        }

        public async Task<User> InsertOrUpdateUserAsync(UserRegistration userRegistration)
        {
            User dbuser = new User()
            {
                FirstName = userRegistration.FirstName,
                LastName = userRegistration.LastName,
                Email = userRegistration.Email,
                Phone = userRegistration.Phone,
                RoleId = userRegistration.RoleId,
                IsBlocked = userRegistration.IsBlocked,
                LastPasswordChangedOn = userRegistration.LastPasswordChangedOn,
                CreatedBy = userRegistration.CreatedBy,
                CreatedOn = userRegistration.CreatedOn,
                ModifiedBy = userRegistration.ModifiedBy,
                ModifiedOn = userRegistration.ModifiedOn,
                IsActive = userRegistration.IsActive
            };

            if (dbuser.Id == Guid.Empty || dbuser.Id == null)
            {
                await _dBContext.users.AddAsync(dbuser);
            }
            else
            {
                var existingUser = await _dBContext.users.FindAsync(dbuser.Id);

                if (existingUser != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingUser, dbuser, nameof(User.CreatedBy), nameof(User.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingUser, dbuser, nameof(User.CreatedBy), nameof(User.CreatedOn));
                    }
                }
            }

            await _dBContext.SaveChangesAsync();

            return dbuser;
        }
    }
}
