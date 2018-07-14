using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCOMSapps.Data;
using TCOMSapps.Extensions;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Titles
{
    [Authorize]
    [Route("/OOSTitles/Titles")]
    public class TitlesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public TitlesController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        [Route("Index")]
        public async Task<ActionResult> Index(string sortOrder, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["VINSortParm"] = String.IsNullOrEmpty(sortOrder) ? "vin_desc" : "";
            ViewData["CustFNameSortParm"] = sortOrder == "custfname" ? "custfname_desc" : "custfname";
            var titles = from t in _db.Titles.Where(t => t.IsDeleted == false)
                         select t;
            switch (sortOrder)
            {
                case "Vin_desc":
                    titles = titles.OrderByDescending(r => r.Id);
                    break;
                case "custfname":
                    titles = titles.OrderBy(r => r.CustFName);
                    break;
                case "custfname_desc":
                    titles = titles.OrderByDescending(r => r.CustFName);
                    break;
                default:
                    titles = titles.OrderBy(r => r.Id);
                    break;
            }
            const int pageSize = 20;
            var titleviewmodel = new TitleViewModel(0, _db, _userManager)
            {
                PaginatedTitles = await PaginatedList<Title>.CreateAsync(titles.AsNoTracking(), page ?? 1, pageSize)
            };
            ViewData["Pages"] = titleviewmodel.PaginatedTitles.TotalPages;
            return View(titleviewmodel);
        }



        [Route("Details")]
        public async Task<ActionResult> Details(int id)
        {
            var vm = new TitleWithTransfersAndInteractionsViewModel(_db, id, _userManager);
            var user = await _userManager.GetUserAsync(User);
            vm.Locations = vm.GetLocationsForCounty(user.CountyId).Where(l => l.Id != vm.GetLastTransferForTitle(id).LocationId).ToList();
            return View(vm);
        }



        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new TitleCreateViewModel(_db, _userManager)
            {
                User = user,
                InitialLocationSelectList = GetLocListItemsForCounty(user.CountyId)
            };
            vm.Title.InitialLocation = vm.GetDefaultCountyLocation(_db).Id;
            return View(vm);
        }
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TitleCreateViewModel vm)
        {
            var user = await _userManager.GetUserAsync(User);
            vm._db = _db;
            vm.User = user;
            vm.InitialLocationSelectList = GetLocListItemsForCounty(user.CountyId);
            if (!ModelState.IsValid) return View(vm);
            var id = vm.AddTitleAndSave(vm, _db);
            vm.AddTranferForNewTitleAndSave(id, _db, vm.Title.InitialLocation);
            return RedirectToAction("Details", "Titles", new { id = vm.Title.Id });
        }



        [Route("Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new StatusCodeResult(400);
            var vm = new TitleViewModel(id, _db, _userManager);
            if (vm.Title == null) return NotFound();
            return View(vm);
        }
        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TitleViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            vm.UpdateTitleAndSave(vm, _db);
            return RedirectToAction("Index", "OosTitlesDashboard");
        }



        [Route("Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new StatusCodeResult(400);
            var vm = new TitleViewModel(id, _db, _userManager);
            if (vm.Title == null) return NotFound();
            return View(vm);
        }
        [HttpPost]
        [Route("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(TitleViewModel vm)
        {
            if (!ModelState.IsValid) return new StatusCodeResult(400);
            vm.DeleteTitleAndSave(vm, _db, _userManager.GetUserAsync(User).Result.Id.ToString());
            return RedirectToAction("Index", "OosTitlesDashboard");
        }



        private IEnumerable<SelectListItem> GetLocListItemsForCounty(int countyid)
        {
            var locations = new List<SelectListItem>();
            foreach (var location in _db.Locations.Where(loc => loc.CountyId == countyid))
            {
                var sli = new SelectListItem()
                {
                    Value = location.Id.ToString(),
                    Text = location.Name
                };
                locations.Add(sli);
            }
            return locations;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}