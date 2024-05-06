using Garage3.Models.Entities;

namespace Garage3.Models.ViewModels
{
    public class ShowVehicleViewModel
    {
       
        public string RegistrationNumber { get; set; }
        public string OwnerFullName { get; set; }
        public string TypeName { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public int NumberOfWheels { get; set; }
        public bool IsParked { get; set; }
    }
}
