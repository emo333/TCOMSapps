using Microsoft.AspNetCore.Identity;
using System;

namespace TCOMSapps.Data
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base()
        { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public int CountyId { get; set; }

        public int RoleTypeId { get; set; }
    }
}
