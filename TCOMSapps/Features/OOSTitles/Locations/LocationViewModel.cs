using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Locations
{
    public class LocationViewModel
    {
        public Location Location { get; set; }

        public IEnumerable<SelectListItem> CountySelectList { get; set; }
    }
}
