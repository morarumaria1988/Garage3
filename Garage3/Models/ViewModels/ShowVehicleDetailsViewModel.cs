using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class ShowVehicleDetailsViewModel
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        [Display(Name = "Wheels")]
        public int NumberOfWheels { get; set; }

        [Display(Name = "time spent parked in Garage")]
        public string? timeSpentParkedInGarage { get; set; }

        [Display(Name = "Historical Arrival - Departure Times")]
        public IEnumerable<ArrivalDepartureTime> historicalArrivalDepartureTimes { get; set; }
    }

    public class ArrivalDepartureTime {

        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}
