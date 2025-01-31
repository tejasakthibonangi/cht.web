using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cht.HMS.Web.Utility
{
    public class ApplicationUser
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid? RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
