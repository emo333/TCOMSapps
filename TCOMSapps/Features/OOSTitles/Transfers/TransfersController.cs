using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TCOMSapps.Data;

namespace TCOMSapps.Features.OOSTitles.Transfers
{
    [Authorize]
    [Route("/OOSTitles/Transfers")]
    public class TransfersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TransfersController(ApplicationDbContext db) { _db = db; }


        [HttpPost]
        [Route("RouteTitle")]
        public JsonResult RouteTitle(int id, string userid)
        {
            _db.Transfers.Find(id).InRouteDt = DateTime.Now;
            _db.Transfers.Find(id).InRouteByUserId = userid;
            _db.SaveChanges();
            return Json("success");
        }


        [HttpPost]
        [Route("ReceiveTitle")]
        public JsonResult ReceiveTitle(int id, string userid)
        {
            _db.Transfers.Find(id).ReceivedDt = DateTime.Now;
            _db.Transfers.Find(id).ReceivedByUserId = userid;
            _db.SaveChanges();
            return Json("success");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing) { _db.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
