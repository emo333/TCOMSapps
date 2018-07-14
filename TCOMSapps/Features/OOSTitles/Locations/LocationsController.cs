using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Locations
{
    [Authorize]
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LocationsController(ApplicationDbContext db) { _db = db; }


        public ActionResult Index() { return View(_db.Locations.ToList()); }



        public ActionResult Create()
        {
            var location = new Location();
            var vm = new LocationViewModel { CountySelectList = GetCountyListItems(), Location = location };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( LocationViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _db.Locations.Add(vm.Location);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Edit(int id)
        {
            var location = _db.Locations.Find(id);
            var vm = new LocationViewModel { CountySelectList = GetCountyListItems(), Location = location };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "Id,Name,CountyId")] LocationViewModel locationvm)
        {
            if (!ModelState.IsValid) return View(locationvm);
            _db.Entry(locationvm.Location).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        private IEnumerable<SelectListItem> GetCountyListItems()
        {
            var counties = new List<SelectListItem>();
            foreach (var county in _db.Counties)
            {
                var sli = new SelectListItem()
                {
                    Value = county.Id.ToString(),
                    Text = county.Name
                };
                counties.Add(sli);
            }
            return counties;
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing) { _db.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
