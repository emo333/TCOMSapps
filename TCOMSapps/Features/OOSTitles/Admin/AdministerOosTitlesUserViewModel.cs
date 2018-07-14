using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Admin
{
    public class AdministerOosTitlesUserViewModel
    {
        public UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public AdministerOosTitlesUserViewModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
                                                RoleManager<ApplicationRole> roleManager,
                                                int countyId)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            OosTitlesCountyUsers = _userManager.Users.Where(u => u.CountyId == countyId).ToList();
            OosTitlesRoles = _roleManager.Roles.Where(u => u.CountyId == countyId).ToList();
            OosTitlesUsers = _userManager.Users.ToList();
            OosTitlesCounties = db.Counties.ToList();
        }

        public ApplicationUser OosTitlesUser { get; set; }

        public ApplicationRole OosTitlesRole { get; set; }

        public County OosTitlesCounty { get; set; }

        public List<ApplicationUser> OosTitlesUsers { get; set; }

        public List<ApplicationRole> OosTitlesRoles { get; set; }

        public List<ApplicationUser> OosTitlesCountyUsers { get; set; }

        public List<County> OosTitlesCounties { get; set; }
    }
}
