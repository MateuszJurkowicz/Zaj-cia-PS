using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Pole Rok urodzenia jest wymagane.")]
        public int YearOfBirth { get; set; }
        public DateTime ActualTime { get; set; }
        public bool Result { get; set; }
    }

}
