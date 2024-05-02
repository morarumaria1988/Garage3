using System.Collections.ObjectModel;

namespace Garage3.Models.Entities
{
    public class Customer
    {
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public ICollection<Vehicle> Vehicles { get; set; }
    }

}
