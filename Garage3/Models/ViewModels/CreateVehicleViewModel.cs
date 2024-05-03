using Garage3.Models.Entities;

namespace Garage3.Models.ViewModels
{
    public class CreateVehicleViewModel
    {
        public string RegistrationNumber { get; set; }
        public string PersonalNumber { get; set; }
        public string VType { get; set; }
        public string? VTypeCustom { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public int NumberOfWheels { get; set; }
        public DateTime? ArrivalTime { get; set; }

    }
}
