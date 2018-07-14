using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TCOMSapps.Data;

namespace TCOMSapps.Features.OOSTitles.Dashboard
{
    [Authorize]
    [Route("/OOSTitles/Dashboard")]
    public class OosTitlesDashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public OosTitlesDashboardController(ApplicationDbContext db,
                                            UserManager<ApplicationUser> usermanager,
                                            RoleManager<ApplicationRole> roleManager)
        {
            _db = db;
            _userManager = usermanager;
            _roleManager = roleManager;
        }


        [Route("Index")]
        public async Task<ActionResult> Index()
        {
            var countyId = _userManager.GetUserAsync(User).Result.CountyId;
            var user = await _userManager.GetUserAsync(User);
            var vm = new OosTitlesDashboardViewModel(_db, _userManager, _roleManager, user, countyId)
            {
                OosTitlesUser = await _userManager.GetUserAsync(User)
            };
            return View(vm);
        }
    }
}