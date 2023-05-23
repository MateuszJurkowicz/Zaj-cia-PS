using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILeapYearInterface _leapYearInterface;
        public IQueryable<Person> Records { get; set; }
        [BindProperty]
        public Person Person { get; set; }
        public bool IsSaved { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ILeapYearInterface leapYearInterface)
        {
            _logger = logger;
            _leapYearInterface = leapYearInterface;
        }
        public void OnGet()
        {
            Records = _leapYearInterface.GetData();

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                IsSaved = false;
                return Page();
            }
            if ((Person.YearOfBirth % 4 == 0) && (Person.YearOfBirth != 1900))
            {
                Person.Result = true;
            }
            else
            {
                Person.Result = false;
            }

            IsSaved = true;
            Person.ActualTime = DateTime.Now;
            _leapYearInterface.AddData(Person);
            return Page();
        }
    }

}