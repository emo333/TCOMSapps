using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCOMSapps.Data;

namespace TCOMSapps.Features.Admin
{
  [Route("/Admin/Admin")]
  [Authorize(Roles = "TCOMS Apps Administrator")]
  public class AdminController : Controller
  {
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ApplicationDbContext ctx,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<AdminController> logger)
    {
      _ctx = ctx;
      _userManager = userManager;
      _roleManager = roleManager;
      _logger = logger;
    }



    [Route("AdminDashboard")]
    public IActionResult AdminDashboard()
    {
      var vm = new AdminDashboardViewModel();
      vm.ApplicationUsers = _ctx.Users.ToList();
      vm.UserSelectList = vm.GetUserSelectList(_ctx);
      return View(vm);
    }




    [Route("RegisterRole")]
    [HttpGet]
    [AllowAnonymous]
    public IActionResult RegisterRole(string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      var vm = new RegisterRoleViewModel
      {
        ApplicationRoles = _roleManager.Roles.ToList(),
        Counties = _ctx.Counties.ToList()
      };
      return View(vm);
    }
    [Route("RegisterRole")]
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterRole(RegisterRoleViewModel model, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      if (!ModelState.IsValid) return View(model);
      var role = new ApplicationRole
      {
        Name = model.Name,
        CountyId = model.CountyId
      };
      var result = await _roleManager.CreateAsync(role);
      if (result.Succeeded)
      {
        _logger.LogInformation("Role created.");
        return RedirectToLocal(returnUrl);
      }
      AddErrors(result);
      return View(model);
    }




    [Route("AddUserToRole")]
    public async Task<ActionResult> AddUserToRole()
    {
      var countyId = (await _userManager.GetUserAsync(User)).CountyId;
      return View(new AdministerUserViewModel(_ctx, _userManager, _roleManager, countyId));
    }
    [Route("AddUserToRole")]
    [HttpPost]
    public async Task<ActionResult> AddUserToRole(AdministerUserViewModel vm)
    {
      await _userManager.AddToRoleAsync(
                                      _userManager.FindByIdAsync(vm.AppUser.Id.ToString()).Result,
                                      _roleManager.FindByIdAsync(vm.AppRole.Id.ToString()).Result.Name
                                      );
      return RedirectToAction("RegisterRole", "Admin");
    }




    [Route("EditUserProfile")]
    public async Task<ActionResult> EditUserProfile(string appUserId)
    {
      var countyId = (await _userManager.GetUserAsync(User)).CountyId;
      var vm = new EditUserProfileViewModel()
      {
        ApplicationUser = await _ctx.Users.FindAsync(new Guid(appUserId)),
        Users = await _ctx.Users.ToListAsync()
      };

      vm.Locations = await _ctx.Locations.ToListAsync();
      vm.Username = appUserId;
      vm.AppRoleNames = await _userManager.GetRolesAsync(await _ctx.Users.FindAsync(new Guid(appUserId))) as List<string>;//await _ctx.Roles.ToListAsync();
      return View(vm);
    }
    [Route("EditUserProfile")]
    [HttpPost]
    public async Task<ActionResult> EditUserProfile(EditUserProfileViewModel vm)
    {
      if (!ModelState.IsValid) return View(vm);
      try
      {
        var emp = await _ctx.Users.FindAsync(new Guid(vm.Username));
        emp.DefaultStationId = vm.ApplicationUser.DefaultStationId;
        emp.DefaultShiftId = vm.ApplicationUser.DefaultShiftId;
        emp.SupervisorId = vm.ApplicationUser.SupervisorId;
        _ctx.Update(emp);
        await _ctx.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }
      return RedirectToAction("RegisterRole", "Admin");
    }




    #region Helpers

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction(nameof(AdminDashboard), "Admin");
      }
    }

    #endregion
  }
}