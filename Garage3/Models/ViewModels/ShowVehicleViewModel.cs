using Garage3.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class ShowVehicleViewModel
    {

        [Display(Name ="Registration number")]
        public string RegistrationNumber { get; set; }
        public string OwnerFullName { get; set; }
        public string TypeName { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }

        [Display(Name ="Wheels")]
        public int NumberOfWheels { get; set; }
        [Display(Name ="Parking status")]
        public bool IsParked { get; set; }
    }
}
