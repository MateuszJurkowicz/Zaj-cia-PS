using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class Historia_wyszukiwanModel : PageModel
    {
        private readonly PeopleContext _context;
        private readonly IConfiguration Configuration;
        //public IList<Person> People { get; set; }

        public Historia_wyszukiwanModel(PeopleContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;

        }
        public PaginatedList<Person> People { get; set; }

        public string IDSort { get; set; }
        public string NameSort { get; set; }
        public string YearSort { get; set; }
        public string DateSort { get; set; }
        public string ResultSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            IDSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            NameSort = sortOrder == "Name" ? "name_desc" : "Name";
            YearSort = sortOrder == "Year" ? "year_desc" : "Year";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ResultSort = sortOrder == "Result" ? "result_desc" : "Result";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Person> PeopleIQ = from s in _context.Person select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                PeopleIQ = PeopleIQ.Where(s => s.FirstName.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "id_desc":
                    PeopleIQ = PeopleIQ.OrderByDescending(s => s.Id);
                    break;
                case "Name":
                    PeopleIQ = PeopleIQ.OrderBy(s => s.FirstName);
                    break;
                case "name_desc":
                    PeopleIQ = PeopleIQ.OrderByDescending(s => s.FirstName);
                    break;
                case "Year":
                    PeopleIQ = PeopleIQ.OrderBy(s => s.YearOfBirth);
                    break;
                case "year_desc":
                    PeopleIQ = PeopleIQ.OrderByDescending(s => s.YearOfBirth);
                    break;
                case "Date":
                    PeopleIQ = PeopleIQ.OrderBy(s => s.ActualTime);
                    break;
                case "date_desc":
                    PeopleIQ = PeopleIQ.OrderByDescending(s => s.ActualTime);
                    break;
                case "Result":
                    PeopleIQ = PeopleIQ.OrderBy(s => s.Result);
                    break;
                case "result_desc":
                    PeopleIQ = PeopleIQ.OrderByDescending(s => s.Result);
                    break;
                default:
                    PeopleIQ = PeopleIQ.OrderBy(s => s.Id);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            People = await PaginatedList<Person>.CreateAsync(PeopleIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public void OnPost()
        {
        }
    }
}

