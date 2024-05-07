using Garage3.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class CreateVehicleViewModel
    {
        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }
        [Display(Name ="Personal number")]
        public string PersonalNumber { get; set; }
        public string VType { get; set; }
        public string? VTypeCustom { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        [Display(Name = "Number of wheels")]
        public int NumberOfWheels { get; set; }
        [Display(Name ="Arrival Time")]
        public DateTime? ArrivalTime { get; set; }

    }
}
