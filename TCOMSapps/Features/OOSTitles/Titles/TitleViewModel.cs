using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TCOMSapps.Data;
using TCOMSapps.Extensions;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Titles
{
    public class TitleViewModel
    {
        private readonly ApplicationDbContext _db;

        public TitleViewModel(int? id, ApplicationDbContext db, UserManager<ApplicationUser> usermanager)
        {
            _db = db;
            UserManager = usermanager;
            Title = _db.Titles.Find(id);
            Transfers = GetTransfersForTitle(id);
            Interactions = GetInteractionsForTitle(id);
        }

        public TitleViewModel()
        {
        }


        public UserManager<ApplicationUser> UserManager { get; set; }

        public Title Title { get; set; }

        public List<Transfer> Transfers { get; set; }

        public List<Interaction> Interactions { get; set; }


        public List<Transfer> GetTransfersForTitle(int? id)
        {
            var transfers = _db.Transfers.ToList().Where(t => t.TitleId == id);
            return transfers.ToList();
        }

        public List<Interaction> GetInteractionsForTitle(int? id)
        {
            var interactions = _db.Interactions.ToList().Where(i => i.TitleId == id);
            return interactions.ToList();
        }

        public PaginatedList<Title> PaginatedTitles { get; set; }




        public void UpdateTitleAndSave(TitleViewModel vm, ApplicationDbContext db)
        {
            db.Entry(vm.Title).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteTitleAndSave(TitleViewModel vm, ApplicationDbContext db, string userid)
        {
            vm.Title.IsDeleted = true;
            vm.Title.DeletedDt = DateTime.Now;
            vm.Title.DeletedBy = userid;
            db.Entry(vm.Title).State = EntityState.Modified;
            db.SaveChanges();
        }

        private readonly List<SelectListItem> _titlerecfromlist = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ((int)Title.TitleReceivedFromTypes.Dealer).ToString(),
                Text = nameof(Title.TitleReceivedFromTypes.Dealer)
            },

            new SelectListItem
            {
                Value = ((int)Title.TitleReceivedFromTypes.Lienholder).ToString(),
                Text = nameof(Title.TitleReceivedFromTypes.Lienholder)
            }

        };
        public IEnumerable<SelectListItem> TitleRecFromSelectList => _titlerecfromlist;

        private readonly List<SelectListItem> _titlerecmethodlist = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ((int)Title.TitleReceivedMethod.Mail).ToString(),
                Text = nameof(Title.TitleReceivedMethod.Mail)
            },

            new SelectListItem
            {
                Value = ((int)Title.TitleReceivedMethod.InPerson).ToString(),
                Text = nameof(Title.TitleReceivedMethod.InPerson)
            }

        };
        public IEnumerable<SelectListItem> TitleRecMethodSelectList => _titlerecmethodlist;
    }
}