using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.Manage
{
    public class IndexViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        public County County { get; set; }

        public string CountyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public IEnumerable<SelectListItem> CountySelectList { get; set; }


        public string GetCountyName(int countyId, ApplicationDbContext db)
        {

            var countyname = db.Counties.Find(countyId).Name;

            return countyname;
        }
    }
}
