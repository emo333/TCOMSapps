using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCOMSapps.Data;
using TCOMSapps.Extensions;
using TCOMSapps.Features.OOSTitles.entities;


namespace TCOMSapps.Features.OOSTitles.SearchTitle
{
  [Authorize]
  public class SearchTitleController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public SearchTitleController(ApplicationDbContext db,
        UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }


    [Route("Search")]
    public ActionResult Search()
    {
      ViewData["CurrentSort"] = "";
      ViewData["VINSortParm"] = "";
      ViewData["CustFNameSortParm"] = "";
      ViewData["Pages"] = 1;
      var vm = new SearchTitleViewModel();
      vm.PaginatedTitles = new PaginatedList<Title>(new List<Title>(), 1, 1, 1);
      return View(vm);
    }



    [HttpPost]
    [Route("Search")]
    public async Task<ActionResult> Search(SearchTitleViewModel search, string sortOrder, int? page)
    {
      var user = await _userManager.GetUserAsync(User);
      var county = _db.Counties.Find(user.CountyId);
      IQueryable<Title> titles;
      ViewData["CurrentSort"] = sortOrder;
      ViewData["VINSortParm"] = String.IsNullOrEmpty(sortOrder) ? "vin_desc" : "";
      ViewData["CustFNameSortParm"] = sortOrder == "custfname" ? "custfname_desc" : "custfname";
      ViewData["CustLNameSortParm"] = sortOrder == "custlname" ? "custlname_desc" : "custlname";
      if (search.Title.Vin != null)
      {
        titles = from t in _db.Titles.Where(t => t.IsDeleted == false && t.Vin == search.Title.Vin && t.CountyId == user.CountyId) select t;
      }
      else if (search.Title.CustFName != null && search.Title.CustLName != null)
      {
        titles = from t in _db.Titles.Where(t => t.IsDeleted == false && t.CustFName == search.Title.CustFName && t.CustLName == search.Title.CustLName && t.CountyId == user.CountyId) select t;
      }
      else if (search.Title.CustFName != null)
      {
        titles = from t in _db.Titles.Where(t => t.IsDeleted == false && t.CustFName == search.Title.CustFName && t.CountyId == user.CountyId) select t;
      }
      else if (search.Title.CustLName != null)
      {
        titles = from t in _db.Titles.Where(t => t.IsDeleted == false && t.CustLName == search.Title.CustLName && t.CountyId == user.CountyId) select t;
      }
      else
      {
        titles = from t in _db.Titles.Where(t => t.IsDeleted == false && t.CountyId == user.CountyId) select t;
      }
      switch (sortOrder)
      {
        case "Vin_desc":
          titles = titles.OrderByDescending(t => t.Id);
          break;
        case "custfname":
          titles = titles.OrderBy(t => t.CustFName);
          break;
        case "custfname_desc":
          titles = titles.OrderByDescending(t => t.CustFName);
          break;
        case "custlname":
          titles = titles.OrderBy(t => t.CustLName);
          break;
        case "custlname_desc":
          titles = titles.OrderByDescending(t => t.CustLName);
          break;
        default:
          titles = titles.OrderByDescending(t => t.RecDt);
          break;
      }
      const int pageSize = 20; //TODO: make dynamic
      var titleviewmodel = new SearchTitleViewModel(_db) { PaginatedTitles = await PaginatedList<Title>.CreateAsync(titles.AsNoTracking(), page ?? 1, pageSize) };
      ViewData["Pages"] = titleviewmodel.PaginatedTitles.TotalPages;
      return View(titleviewmodel);
    }



    //todo: possibly delete
    public async Task<List<Title>> GetCountyTitles(IQueryable<Title> titles)
    {
      var user = await _userManager.GetUserAsync(User);
      var countytitles = titles.ToList();
      var locations = _db.Locations.Where(l => l.CountyId == user.CountyId);
      foreach (var location in locations)
      {
        foreach (var title in titles)
        {
          if (title.InitialLocation != location.Id)
          {
            //countytitles.Remove(title);
          }
        }
      }
      return countytitles;
    }
  }
}