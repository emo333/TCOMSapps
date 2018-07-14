using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Titles
{
    public class TitleCreateViewModel
    {
        public ApplicationDbContext _db;

        public TitleCreateViewModel(ApplicationDbContext db, UserManager<ApplicationUser> usermanager)
        {
            _db = db;
            UserManager = usermanager;
            User = usermanager.Users.FirstOrDefault();
            Title = new Title
            {
                EntryDt = DateTime.Now,
                InitialLocation = _db.Locations.Find(GetDefaultCountyLocation().Id).Id
            };
        }

        public TitleCreateViewModel()
        {
        }


        public ApplicationUser User { get; set; }

        public UserManager<ApplicationUser> UserManager { get; set; }

        public IEnumerable<SelectListItem> InitialLocationSelectList { get; set; }

        public Title Title { get; set; }


        public Location GetDefaultCountyLocation()
        {
            var defaultlocation = new Location();
            var county = _db.Counties.Find(User.CountyId);
            if (county != null)
            {
                defaultlocation = _db.Locations.Find(county.DefaultLocation);
            }
            return defaultlocation;
        }



        public Location GetDefaultCountyLocation(ApplicationDbContext db)
        {
            var defaultlocation = new Location();
            var county = db.Counties.Find(User.CountyId);
            if (county != null)
            {
                defaultlocation = db.Locations.Find(county.DefaultLocation);
            }
            return defaultlocation;
        }



        public int AddTitleAndSave(TitleCreateViewModel vm, ApplicationDbContext db)
        {
            vm.Title.CountyId = db.Counties.Find(User.CountyId).Id;
            vm.Title.UserId = User.Id.ToString();
            vm.Title.CustFName = vm.Title.CustFName.ToUpper();
            if (vm.Title.CustFName2 != null) vm.Title.CustFName2 = vm.Title.CustFName2.ToUpper();
            if (vm.Title.CustFName3 != null) vm.Title.CustFName3 = vm.Title.CustFName3.ToUpper();
            vm.Title.CustLName = vm.Title.CustLName.ToUpper();
            if (vm.Title.CustLName2 != null) vm.Title.CustLName2 = vm.Title.CustLName2.ToUpper();
            if (vm.Title.CustLName3 != null) vm.Title.CustLName3 = vm.Title.CustLName3.ToUpper();
            vm.Title.CustAddr1 = vm.Title.CustAddr1.ToUpper();
            if (vm.Title.CustAddr2 != null) vm.Title.CustAddr2 = vm.Title.CustAddr2.ToUpper();
            vm.Title.CustCity = vm.Title.CustCity.ToUpper();
            vm.Title.CustState = vm.Title.CustState.ToUpper();
            if (vm.Title.CustEmail != null) vm.Title.CustEmail = vm.Title.CustEmail.ToLower();
            vm.Title.Vin = vm.Title.Vin.ToUpper();
            vm.Title.TitleState = vm.Title.TitleState.ToUpper();
            vm.Title.VehMake = vm.Title.VehMake.ToUpper();
            vm.Title.VehModel = vm.Title.VehModel.ToUpper();
            db.Titles.Add(vm.Title);
            db.SaveChanges();
            var id = vm.Title.Id;
            return id;
        }



        public void AddTranferForNewTitleAndSave(int titleid, ApplicationDbContext db, int locationid)
        {
            var transfer = new Transfer
            {
                LocationId = locationid,
                TitleId = titleid,
                TransferRequestedDt = DateTime.Now,
                TransferRequestUserId = User.Id.ToString(),

                IsInRoute = true,
                InRouteDt = DateTime.Now,
                InRouteByUserId = User.Id.ToString(),

                IsReceived = true,
                ReceivedDt = DateTime.Now,
                ReceivedByUserId = User.Id.ToString()
            };
            db.Transfers.Add(transfer);
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