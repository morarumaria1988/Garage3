using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.Entities
{
    public class Customer
    {
        [Display(Name = "Personal number")]
        [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "Please enter a valid Swedish personal number (YYMMDD-XXXX).")]
        public string PersonalNumber { get; set; }
        [Display(Name ="First name:")]
        public string FirstName { get; set; }
        [Display(Name ="Last name:")]
        public string LastName { get; set; }
        [Display(Name = "Name:")]
        public string FullName => FirstName + " " + LastName;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }

}
