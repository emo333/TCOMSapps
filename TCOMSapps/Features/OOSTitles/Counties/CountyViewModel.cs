using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TCOMSapps.Data;

namespace TCOMSapps.Features.OOSTitles.Counties
{
    
    
    public class CountyViewModel
    {

        CountyViewModel(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }


        public UserManager<ApplicationUser> UserManager { get; set; }


        public void GetUserNameByUserId(string userid)
        {
            var user = UserManager.FindByIdAsync(userid);
        }
    }
}
