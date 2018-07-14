using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Counties
{
    [Authorize]
    [Route("/OOSTitles/Counties")]
    public class CountiesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CountiesController(ApplicationDbContext db)
        {
            _db = db;
        }


        [Route("Index")]
        public ActionResult Index()
        {
            return View(_db.Counties.ToList());
        }



        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "Id,Name,PocUserId,SharedData,Active")] County county)
        {
            if (!ModelState.IsValid) return View(county);
            _db.Counties.Add(county);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        [Route("Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new StatusCodeResult(400);
            var county = _db.Counties.Find(id);
            if (county == null) return NotFound();
            return View(county);
        }
        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "Id,Name,PocUserId,SharedData,Active")] County county)
        {
            if (!ModelState.IsValid) return View(county);
            _db.Entry(county).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
