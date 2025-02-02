using AspNetCoreHero.ToastNotification.Abstractions;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cht.HMS.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        private readonly ApplicationUser _applicationUser;
        public UserController(IUserService userService,
            INotyfService notyfService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _notyfService = notyfService;
            string appUser = httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");
            _applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(appUser);
        }
        [HttpGet]
        public async Task<IActionResult> FetchUsers()
        {
            try
            {
                var response = await _userService.FetchUsersAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUser([FromBody] UserRegistration registration)
        {
            try
            {
                await _userService.InsertOrUpdateUserAsync(registration);
                _notyfService.Success("User creation successful");
                return Json(new { data = true });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
