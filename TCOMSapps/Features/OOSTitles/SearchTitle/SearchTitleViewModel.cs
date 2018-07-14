using System.Collections.Generic;
using TCOMSapps.Data;
using TCOMSapps.Extensions;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.SearchTitle
{
    public class SearchTitleViewModel
    {
        private readonly ApplicationDbContext _db;
        public SearchTitleViewModel(ApplicationDbContext db)
        {
            _db = db;
            Title = new Title { Transfers = new List<Transfer>() };
            Titles = new List<Title>();
        }

        public SearchTitleViewModel()
        {
            Title = new Title { Transfers = new List<Transfer>() };
            Titles = new List<Title>();
        }

        public Title Title { get; set; }
        public List<Title> Titles { get; set; }
        public PaginatedList<Title> PaginatedTitles { get; set; }

    }
}