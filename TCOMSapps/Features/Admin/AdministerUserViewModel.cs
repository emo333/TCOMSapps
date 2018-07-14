using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.Admin
{
    public class AdministerUserViewModel
    {
        public AdministerUserViewModel() { }

        public AdministerUserViewModel(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            int countyId)
        {
            var userManager1 = userManager;
            CountyUsers = userManager1.Users.Where(u => u.CountyId == countyId).ToList();
            AppRoles = roleManager.Roles.Where(u => u.CountyId == countyId).ToList();
            AppUsers = userManager1.Users.ToList();
            Counties = db.Counties.ToList();
        }

        public ApplicationUser AppUser { get; set; }

        public ApplicationRole AppRole { get; set; }

        public County County { get; set; }

        public List<ApplicationUser> AppUsers { get; set; }

        public List<ApplicationRole> AppRoles { get; set; }

        public List<ApplicationUser> CountyUsers { get; set; }

        public List<County> Counties { get; set; }
    }
}
