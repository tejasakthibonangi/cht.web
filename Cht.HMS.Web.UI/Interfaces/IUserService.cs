using Cht.HMS.Web.UI.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IUserService
    {
        Task<User> InsertOrUpdateUserAsync(UserRegistration user);

        Task<IEnumerable<UserInfirmation>> FetchUsersAsync();
    }
}
