using Cht.HMS.Web.API.Filters;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ChtAuthorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("InsertOrUpdateUserAsync")]
        public async Task<IActionResult> InsertOrUpdateUserAsync(UserRegistration user)
        {
            try
            {
                var response = await _userManager.InsertOrUpdateUserAsync(user);


                return Ok(response);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
        [HttpGet]
        [Route("FetchUsersAsync")]
        public async Task<IActionResult> FetchUsersAsync()
        {
            try
            {
                var response = await _userManager.FetchUsersAsync();


                return Ok(response);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
