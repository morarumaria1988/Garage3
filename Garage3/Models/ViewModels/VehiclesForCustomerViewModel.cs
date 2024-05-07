using Garage3.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class VehiclesForCustomerViewModel
    {
        public class VehicleViewModel
        {
            [Display(Name ="Registration number")]
            public string RegistrationNumber { get; set; }
            public string PersonalNumber { get; set; }
            public string TypeName { get; set; }
            public string VType { get; set; }
            public string Color { get; set; }
            public string Make { get; set; }

            [Display(Name ="Wheels")]
            public int NumberOfWheels { get; set; }

        }
        public class CustomerViewModel {
            public string PersonalNumber { get; set; }
            public string FullName { get; set; }
        }
        public IEnumerable<VehicleViewModel> vehicleViewModels { get; set; }

        public CustomerViewModel customer { get; set; }
    }

   
}
