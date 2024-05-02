namespace Garage3.Models.Entities
{
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        VehicleType Vehicle_type { get; set; }
        public string Color { get; set; }
        public string Make {  get; set; }
        public int NumberOfWheels { get; set; }
        public Customer Member { get; set; }
        public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
        public DateTime? ArrivalTime { get; set; } 
    }
    public class VehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
