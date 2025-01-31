using Cht.HMS.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Cht.HMS.Web.UI.Factory
{
    public class UserPrincipal
    {
        public static ClaimsPrincipal GenarateUserPrincipal(ApplicationUser user)
        {

            var claims = new List<Claim>
          {
             new Claim("Id", user.Id.ToString()),
             new Claim("Phone", user.Phone),
             new Claim("Email", user.Email),
             new Claim("FullName", user.FullName),
             new Claim("FirstName", user.FirstName),
             new Claim("LastName", user.LastName),
             new Claim("RoleId", user.RoleId.ToString()),
             new Claim(ClaimTypes.Role,user.RoleName)
          };
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            return principal;
        }
        private static string MapRoleIdToRoleName(long? roleId)
        {
            switch (roleId)
            {
                case 1000:
                    return "Administrator";
                case 1001:
                    return "Admin";
                case 1003:
                    return "Test";
                case 1004:
                    return "Software Engineer/Developer";
                case 1005:
                    return "Quality Assurance Engineer/Tester";
                case 1006:
                    return "Project Manager";
                case 1007:
                    return "Product Manager";
                case 1008:
                    return "UX/UI Designer";
                case 1009:
                    return "Data Scientist";
                case 1010:
                    return "DevOps Engineer";
                case 1011:
                    return "Technical Support Engineer";
                case 1012:
                    return "Technical Writer/Documentation Specialist";
                case 1013:
                    return "Business Analyst";
                case 1014:
                    return "Scrum Master";
                case 1015:
                    return "System Administrator";
                case 1016:
                    return "Network Engineer";
                case 1017:
                    return "Security Engineer";
                case 1018:
                    return "Release Manager";
                case 1019:
                    return "Database Administrator";
                case 1020:
                    return "Software Architect";
                case 1021:
                    return "Front-end Developer";
                case 1022:
                    return "Back-end Developer";
                case 1023:
                    return "Full-stack Developer";
                case 1024:
                    return "Mobile Application Developer";
                case 1025:
                    return "AI/Machine Learning Engineer";
                case 1026:
                    return "Cloud Engineer";
                case 1027:
                    return "Automation Engineer";
                case 1028:
                    return "IT Support Specialist";
                case 1029:
                    return "Test";
                case 1030:
                    return "Test 2";
                default:
                    return "Unknown";
            }
        }
    }
}
