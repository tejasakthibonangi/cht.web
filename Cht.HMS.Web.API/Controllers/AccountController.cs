using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        public AccountController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        [Route("AuthenticateUserAsync")]
        public async Task<IActionResult> AuthenticateUserAsync(UserAuthentication authentication)
        {
            try
            {
                var response = await _authenticationManager.AuthenticateUserAsync(authentication);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("GenarateUserClaimsAsync")]
        public async Task<IActionResult> GenarateUserClaimsAsync(AuthResponse authentication)
        {
            try
            {
                var response = await _authenticationManager.GenarateUserClaimsAsync(authentication);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}