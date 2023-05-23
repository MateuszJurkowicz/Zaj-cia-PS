using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ILeapYearInterface
    {
        public IQueryable<Person> GetData();
        public void AddData(Person person);
    }
}
