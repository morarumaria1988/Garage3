using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage3.Models.Entities
{
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string PersonalNumber { get; set; }
        public string TypeName { get; set; }
        public VehicleType? VType { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        [Display(Name = "Wheels")]
        public int NumberOfWheels { get; set; }
        public Customer? Member { get; set; }
        public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
        [Display(Name = "Arrival time")]
        public DateTime? ArrivalTime { get; set; }

        public String? TimeSpentSinceArrivalTime { 
            
            get
            {
                String timeSpent = null;
                if (ArrivalTime is DateTime arrivalTime)
                {
                    var diff = DateTime.Now.Subtract(arrivalTime);
                    timeSpent = String.Format("{0} hrs {1} mins", diff.Hours, diff.Minutes);
                }

                return timeSpent;

            }
    }

}
public class VehicleType
    {
        [Key]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
