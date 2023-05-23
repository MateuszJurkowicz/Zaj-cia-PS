using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class LeapYearInterface : ILeapYearInterface
    {
        private readonly PeopleContext _context;
        public LeapYearInterface(PeopleContext context)
        {
            _context = context;
        }
        public IQueryable<Person> GetData()
        {
            return _context.Person.Where(p => p.Result);
        }
        public void AddData(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
        }
    }
}
