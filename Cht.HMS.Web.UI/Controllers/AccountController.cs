using AspNetCoreHero.ToastNotification.Abstractions;
using Cht.HMS.Web.UI.Factory;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Cht.HMS.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IAuthenticateService authenticateService,
            INotyfService notyfService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authenticateService = authenticateService;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Clear session data
                HttpContext.Session.Clear();

                // Clear all cookies
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                // Sign out the user
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            }

            return View();

        }
        [HttpPost]
        public async Task<JsonResult> Login([FromBody] UserAuthentication authentication)
        {
            try
            {
                var responce = await _authenticateService.AuthenticateUserAsync(authentication);
                //check the response here

                if (responce != null)
                {
                    if (!string.IsNullOrEmpty(responce.JwtToken))
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("AccessToken", responce.JwtToken);

                        var userClaimes = await _authenticateService.GenarateUserClaimsAsync(responce);

                        _httpContextAccessor.HttpContext.Session.SetString("ApplicationUser", JsonConvert.SerializeObject(userClaimes));

                        var claimsIdentity = UserPrincipal.GenarateUserPrincipal(userClaimes);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                           new ClaimsPrincipal(claimsIdentity),
                                                           new AuthenticationProperties
                                                           {
                                                               IsPersistent = true,
                                                               ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                                                           });

                        return Json(new { appUser = userClaimes, status = true });
                    }

                    _notyfService.Error(responce.StatusMessage);
                }
                else
                {
                    _notyfService.Error("Something went wrong");
                }

                return Json(new { appUser = default(object), status = false });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }

        }
        public async Task<IActionResult> Logout()
        {
            var appuser = _httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");
            HttpContext.Session.Remove("AccessToken");
            HttpContext.Session.Remove("ApplicationUser");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("allowCookies");
            return RedirectToAction("Login", "Account", null);
        }
    }
}
