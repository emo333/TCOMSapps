using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TCOMSapps.Data;

namespace TCOMSapps.Features.OOSTitles.Admin
{


  [Authorize]
  [Route("/OOSTitles/Admin")]
  public class AdminController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
      _db = db;
      _userManager = userManager;
      _roleManager = roleManager;
    }



    [Route("Index")]
    public async Task<ActionResult> Index()
    {
      var countyId = _userManager.GetUserAsync(User).Result.CountyId;
      var vm = new AdministerOosTitlesUserViewModel(_db, _userManager, _roleManager, countyId)
      {
        OosTitlesUser = await _userManager.GetUserAsync(User)
      };
      return View(vm);
    }


    [Route("AddOosTitleUserToRole")]
    public async Task<ActionResult> AddOosTitleUserToRole()
    {
      var countyId = _userManager.GetUserAsync(User).Result.CountyId;
      var vm = new AdministerOosTitlesUserViewModel(_db, _userManager, _roleManager, countyId)
      {
        OosTitlesUser = await _userManager.GetUserAsync(User)
      };
      return View(vm);
    }
    [HttpPost]
    public async Task<ActionResult> AddOosTitleUserToRole(Guid oosTitlesUser, Guid oosTitlesRole)
    {
      var user = _userManager.FindByIdAsync(oosTitlesUser.ToString()).Result;
      var role = _roleManager.FindByIdAsync(oosTitlesRole.ToString()).Result;
      await _userManager.AddToRoleAsync(user, role.Name);
      return RedirectToAction("Index", "Admin");
    }
  }
}