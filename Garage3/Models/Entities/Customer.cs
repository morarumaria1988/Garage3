using System.Collections.ObjectModel;
using Garage3.Validation;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.Entities
{
    public class Customer
    {

        [CheckPersonalNumber]
        [Display(Name = "Personal number")]
        public string PersonalNumber { get; set; }
        [Display(Name ="First name:")]
        [CheckFirstName]
        public string FirstName { get; set; }
        [Display(Name ="Last name:")]
        public string LastName { get; set; }
        [Display(Name = "Name:")]
        public string FullName => FirstName + " " + LastName;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }

}
