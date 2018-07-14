using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;
using TCOMSapps.Features.OOSTitles.entities.DTOs;

namespace TCOMSapps.Features.OOSTitles.Titles
{
    public class TitleWithTransfersAndInteractionsViewModel
    {

        private readonly ApplicationDbContext _db;
        public TitleWithTransfersAndInteractionsViewModel(ApplicationDbContext db, int? id, UserManager<ApplicationUser> userManager)
        {
            Title = db.Titles.Find(id);
            Transfers = GetTransfersForTitle(db, id);
            Interactions = GetInteractionsForTitle(db, id);
            LocationName = db.Locations.Find(Title.InitialLocation).Name;
            UserManager = userManager;
            _db = db;
        }


        public Title Title { get; set; }

        public List<TransferDto> Transfers { get; set; }

        public List<Interaction> Interactions { get; set; }

        public List<Location> Locations { get; set; }

        [DisplayName("Location")]
        public string LocationName { get; set; }

        public int NewLocation { get; set; }

        public UserManager<ApplicationUser> UserManager { get; set; }



        public List<Location> GetLocationsForCounty(int countyid)
        {
            var locations = _db.Locations.Where(l => l.CountyId == countyid).ToList();

            return locations;
        }



        public List<TransferDto> GetTransfersForTitle(ApplicationDbContext db, int? id)
        {
            var transfersContext = db.Transfers.ToList().Where(t => t.TitleId == id);
            var transfersDto = new List<TransferDto>();
            foreach (var transferContext in transfersContext)
            {
                var transferDto = new TransferDto
                {
                    Id = transferContext.Id,
                    InRouteByUserId = transferContext.InRouteByUserId,
                    InRouteDt = transferContext.InRouteDt,
                    IsInRoute = transferContext.IsInRoute,
                    IsReceived = transferContext.IsReceived,
                    LocationId = transferContext.LocationId,
                    Method = transferContext.Method,
                    Notes = transferContext.Notes,
                    ReceivedByUserId = transferContext.ReceivedByUserId,
                    ReceivedDt = transferContext.ReceivedDt,
                    TitleId = transferContext.TitleId,
                    TransferRequestUserId = transferContext.TransferRequestUserId,
                    TransferRequestedDt = transferContext.TransferRequestedDt,
                    LocationName = db.Locations.Find(transferContext.LocationId).Name
                };
                transfersDto.Add(transferDto);
            }
            return transfersDto.ToList();
        }



        public List<Interaction> GetInteractionsForTitle(ApplicationDbContext db, int? id)
        {
            var interactions = db.Interactions.ToList().Where(i => i.TitleId == id);
            return interactions.ToList();
        }



        public Transfer GetLastTransferForTitle(int id)
        {
            var lasttransfer = (_db.Transfers.Where(t => t.TitleId == id)).Last();
            return lasttransfer;
        }
    }
}