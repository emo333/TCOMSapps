using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using TCOMSapps.Data;

namespace TCOMSapps.Features.Admin
{
    public class AdminDashboardViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        
        public List<ApplicationUser> ApplicationUsers { get; set; }
        
        public SelectList UserSelectList { get; set; }


        public SelectList GetUserSelectList(ApplicationDbContext ctx)
        {
            var ul = new SelectList(ctx.Users, "Id", "Name");
            foreach (var u in ctx.Users)
            {
                var s = new SelectListItem()
                {
                    Value = u.Id.ToString(), 
                    Text = u.FirstName + " " + u.LastName
                };
                ul.Append(s);
            }
            return ul;
        }
    }
}
