namespace Garage3.Models.Entities
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public Vehicle Fordon { get; set; }
        public double Price { get; set; }
        public string RegistrationNumber { get; set; }

    }
}
