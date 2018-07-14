using Microsoft.AspNetCore.Mvc;
using System;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Titles
{
    [Route("/OOSTitles/Titles/SendToLocationApi")]
    public class SendToLocationApiController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SendToLocationApiController(ApplicationDbContext db) { _db = db; }

        [HttpPost]
        public JsonResult SendTitleToLocation(ApiSendTitleDto title)
        {

            if (title.Id == "dmv")
            {
                var tit =_db.Titles.Find(title.TitleId);
                tit.SentToDmvDt = DateTime.Now;
                tit.SentUserId = title.UserId;
                tit.FollowUp = false;
                _db.SaveChanges();
                return Json(title);
            }

            if (title.Id == "dealerlien")
            {
                var tit = _db.Titles.Find(title.TitleId);
                tit.SentBackToLhDlrDt = DateTime.Now;
                tit.SentUserId = title.UserId;
                tit.FollowUp = false;
                _db.SaveChanges();
                return Json(title);
            }

            var newtransfer = AddTranferForTitleAndSave(title.TitleId, _db, int.Parse(title.Id), title.UserId);
            return Json(newtransfer);
        }


        public Transfer AddTranferForTitleAndSave(int titleid, ApplicationDbContext db, int locationid, string userid)
        {
            var transfer = new Transfer
            {
                TransferRequestedDt = DateTime.Now,
                LocationId = locationid,
                TitleId = titleid,
                IsReceived = false,
                TransferRequestUserId = userid
            };
            db.Transfers.Add(transfer);
            db.SaveChanges();
            return transfer;
        }
    }


    public class ApiSendTitleDto
    {
        public int TitleId { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}