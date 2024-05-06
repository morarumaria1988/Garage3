using System.Collections.ObjectModel;
using Garage3.Validation;

namespace Garage3.Models.Entities
{
    public class Customer
    {
        [CheckPersonalNumber]
        public string PersonalNumber { get; set; }
        [CheckFirstName]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }

}
