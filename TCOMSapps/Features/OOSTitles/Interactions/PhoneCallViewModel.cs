using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Interactions
{
    public class PhoneCallViewModel
    {
        private readonly ApplicationDbContext _db;

        public PhoneCallViewModel(int? id, ApplicationDbContext db, UserManager<ApplicationUser> usermanager)
        {
            _db = db;
            UserManager = usermanager;
            Interaction = _db.Interactions.Find(id);
        }

        public PhoneCallViewModel()
        {
        }


        public UserManager<ApplicationUser> UserManager { get; set; }

        public Interaction Interaction { get; set; }


        public List<Location> Locations { get; set; }

        public int SelectedLocation { get; set; }



        public List<Location> GetLocationsForCounty(int countyid)
        {
            var locations = _db.Locations.Where(l => l.CountyId == countyid).ToList();

            return locations;
        }

    }
}
