using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;
using TCOMSapps.Features.OOSTitles.entities.DTOs;

namespace TCOMSapps.Features.OOSTitles.Dashboard
{
    public class OosTitlesDashboardViewModel
    {
        private readonly ApplicationDbContext _db;

        public OosTitlesDashboardViewModel(ApplicationDbContext db,
            UserManager<ApplicationUser> usermanager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationUser user,
            int countyId)
        {
            _db = db;
            UserManager = usermanager;
            RoleManager = roleManager;
            User = user;
            TitlesWithFollowup = GetTitlesWithActiveFollowup(user.CountyId);
            TitlesWithOverdue = GetTitlesOverdue(user).ToList();
            TitlesNeedReturned = GetTitlesNeedReturned(user).ToList();
            OosTitlesRoles = roleManager.Roles.Where(u => u.CountyId == countyId).ToList();
        }


        public ApplicationUser User { get; set; }

        public List<Title> TitlesWithOverdue { get; set; }

        public List<Title> TitlesNeedReturned { get; set; }

        public List<Title> TitlesWithFollowup { get; set; }

        public UserManager<ApplicationUser> UserManager { get; set; }

        public RoleManager<ApplicationRole> RoleManager { get; set; }

        public ApplicationUser OosTitlesUser { get; set; }

        public List<ApplicationRole> OosTitlesRoles { get; set; }



        public List<Title> GetTitlesOverdue(ApplicationUser user)
        {
            var listOfOverdueTitles = new List<Title>();
            var titelesNotDeletedOrSent = _db.Titles.Where(t => t.DeletedDt == null && t.SentBackToLhDlrDt == null && t.SentToDmvDt == null);
            var twOverdueIds = titelesNotDeletedOrSent.Join(_db.Interactions, tit => tit.LastInteraction, inter => inter.Id,
                        (tit, inter) => new { inter.CustLName, inter.InteractionType, inter.InteractionDt, inter.TitleId })
                    .Where(i => i.InteractionType != "Warning Letter" && i.InteractionDt < DateTime.Today.AddDays(-30));
            foreach (var titleObj in twOverdueIds)
            {
                listOfOverdueTitles.Add(_db.Titles.Find(titleObj.TitleId));
            }
            var titsWnoInter = titelesNotDeletedOrSent.Where(t => t.LastInteraction == 0 && t.RecDt < DateTime.Today.AddDays(-30));
            foreach (var title in titsWnoInter)
            {
                listOfOverdueTitles.Add(_db.Titles.Find(title.Id));
            }
            return listOfOverdueTitles;
        }


        public List<Title> GetTitlesNeedReturned(ApplicationUser user)
        {
            var interactionlist = (from interaction in _db.Interactions
                                   where interaction.InteractionType == "Warning Letter" && interaction.InteractionType != "Title Return Letter"
                                   group interaction by interaction.TitleId into g
                                   select g.Last()
                ).ToList().Where(i => i.InteractionDt < DateTime.Today.AddDays(-10));
            return interactionlist.Select(interaction => GetActiveTitles(User)
                .Find(t => t.Id == interaction.TitleId)).Where(ts => ts != null).ToList();
        }



        public List<Title> GetTitlesWithActiveFollowup(int countyid)
        {
            var titlelist = new List<Title>();
            var futitlelist = (from titles in _db.Titles
                               where titles.FollowUp
                               select titles
                ).ToList();
            var locations = GetAllCountyLocations(countyid);
            foreach (var location in locations)
            {
                foreach (var title in futitlelist)
                {
                    if (title.InitialLocation == location.Id)
                    {
                        titlelist.Add(title);
                    }
                }
            }
            return titlelist;
        }



        public List<TitleDto> GetTitlesAtLocation(int id)
        {
            var transferlist = from transfer in _db.Transfers
                               where transfer.InRouteDt != null
                               group transfer by transfer.TitleId into currenttransfers
                               select currenttransfers.OrderByDescending(t => t.TransferRequestedDt).FirstOrDefault();
            var titlelist = (from titles in GetActiveTitles(User)
                             join transfers in transferlist on titles.Id equals transfers.TitleId
                             where transfers.LocationId == id
                             select titles
                            ).OrderBy(t => t.CustFName).ThenBy(t => t.CustLName).ToList();
            var titleDtoList = new List<TitleDto>();
            foreach (var title in titlelist)
            {
                var titleDto = title.MapToTitleDto(title);
                if (title.Transfers.Find(tr => tr.InRouteDt == null) != null)
                {
                    titleDto.RequestedNotRouted = true;
                    titleDtoList.Add(titleDto);
                }
                else if (title.Transfers.Find(tr => tr.ReceivedDt == null) != null)
                {
                    titleDto.RoutedNotReceived = true;
                    titleDtoList.Add(titleDto);
                }
                else { titleDtoList.Add(titleDto); }
            }
            return titleDtoList;
        }



        public List<Location> GetAllCountyLocations(int county)
        {
            var locations = _db.Locations.Where(l => l.CountyId == county).ToList();
            return locations;
        }



        public List<Location> GetCurrentCountyLocations(Guid currentuserid)
        {
            var countyid = _db.Users.Find(currentuserid).CountyId;
            var locations = _db.Locations.Where(l => l.CountyId == countyid).ToList();
            return locations;
        }



        public List<Title> GetActiveTitles(ApplicationUser user)
        {
            var titlelist = new List<Title>();
            var titles = _db.Titles.Include(t => t.Transfers).Where(t => t.IsDeleted == false &&
                                                 t.SentBackToLhDlrDt == null &&
                                                 t.SentToDmvDt == null
            ).ToList();
            foreach (var location in GetCurrentCountyLocations(user.Id))
            {
                titlelist.AddRange(titles.Where(t => t.InitialLocation == location.Id).ToList());
            }
            return titlelist;
        }
    }
}