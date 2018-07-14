using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.Admin
{
  public class EditUserProfileViewModel
  {

    public ApplicationUser ApplicationUser { get; set; }

    public Guid Id { get; set; }

    public string Username { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }

    public string StatusMessage { get; set; }

    public County County { get; set; }

    public string CountyName { get; set; }

    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [DisplayName("Last Name")]
    public string LastName { get; set; }

    public List<ApplicationUser> Users { get; set; }

    public List<Location> Locations { get; set; }

    public List<string> AppRoleNames { get; set; }

    public IEnumerable<SelectListItem> CountySelectList { get; set; }


    public string GetCountyName(int countyId, ApplicationDbContext db)
    {
      var countyname = db.Counties.Find(countyId).Name;
      return countyname;
    }
  }



}
