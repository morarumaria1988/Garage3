using System.ComponentModel.DataAnnotations;

namespace Garage3.Models
{
    public class CustomerIndexViewModel
    {
        [Display(Name="Personal number ")]
        public string PersonalNumber { get; set; }
        [Display(Name="First name")]
        public string FirstName { get; set; }
        [Display(Name="Last name")]
        public string LastName { get; set; }
        [Display(Name= "Full name")]
        public string FullName => $"{FirstName} {LastName}";
        [Display(Name="Vehicles owned")]
        public int VehicleCount { get; set; }
    }
}
