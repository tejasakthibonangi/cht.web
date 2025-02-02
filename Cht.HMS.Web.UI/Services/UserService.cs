using Cht.HMS.Web.UI.Factory;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory _repository;

        public UserService(IRepositoryFactory repository)
        {
            _repository = repository;

        }
        public async Task<IEnumerable<UserInfirmation>> FetchUsersAsync()
        {
            return await _repository.SendAsync<IEnumerable<UserInfirmation>>(HttpMethod.Get, "User/FetchUsersAsync");
        }

        public async Task<User> InsertOrUpdateUserAsync(UserRegistration user)
        {
            return await _repository.SendAsync<UserRegistration, User>(HttpMethod.Post, "User/InsertOrUpdateUserAsync", user);
        }
    }
}
