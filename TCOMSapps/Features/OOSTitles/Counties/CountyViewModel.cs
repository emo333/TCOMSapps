using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Counties
{


  public class CountyViewModel
  {
    public CountyViewModel(UserManager<ApplicationUser> userManager)
    {
      UserManager = userManager;
    }

    public CountyViewModel()
    {
      
    }


    public UserManager<ApplicationUser> UserManager { get; set; }


    public void GetUserNameByUserId(string userid)
    {
      var user = UserManager.FindByIdAsync(userid);
    }

    public County County { get; set; }

    public List<Location> DefaultLocations { get; set; }
  }
}
