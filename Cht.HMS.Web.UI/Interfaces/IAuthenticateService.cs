using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IAuthenticateService
    {
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
    }
}
