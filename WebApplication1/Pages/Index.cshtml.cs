using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PeopleContext _context;
        [BindProperty]
        public Person Person { get; set; }
        public IList<Person> People { get; set; }
        public bool IsSaved { get; set; }
        public IndexModel(ILogger<IndexModel> logger, PeopleContext context)
        {
            _logger = logger;
            _context = context;
        }


        public void OnGet()
        {
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
            _context.Person.Add(Person);
            _context.SaveChanges();
            return Page();
        }
    }

}