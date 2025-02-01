using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.API.Manager
{
    public interface IUserManager
    {
        Task<User> InsertOrUpdateUserAsync(UserRegistration user);

        Task<IEnumerable<UserInfirmation>> FetchUsersAsync();

        Task<ApplicationUser> GetCurrentUserAsync(string email);
    }
}
