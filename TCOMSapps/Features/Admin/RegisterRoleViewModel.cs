using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.Admin
{
    public class RegisterRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "County")]
        public int CountyId { get; set; }

        public List<ApplicationRole> ApplicationRoles { get; set; }

        public List<County> Counties { get; set; }

    }
}
