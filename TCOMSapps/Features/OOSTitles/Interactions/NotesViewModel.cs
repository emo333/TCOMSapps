using Microsoft.AspNetCore.Identity;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Interactions
{
    public class NotesViewModel
    {
        public NotesViewModel(int? id, ApplicationDbContext db, UserManager<ApplicationUser> usermanager)
        {
            UserManager = usermanager;
            Interaction = db.Interactions.Find(id);
        }

        public NotesViewModel()
        {
        }


        public UserManager<ApplicationUser> UserManager { get; set; }

        public Interaction Interaction { get; set; }

    }
}