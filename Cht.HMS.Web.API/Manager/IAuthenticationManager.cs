using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.API.Manager
{
    public interface IAuthenticationManager
    {
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
    }
}
