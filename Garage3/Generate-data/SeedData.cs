using Bogus.DataSets;
using Bogus;
using System.Globalization;
using Garage3.Models;
using Garage3.Models.Persistence;
using Garage3.Models.Entities;
using Bogus.Extensions.Sweden;
using Microsoft.EntityFrameworkCore;
using Azure;
using Bogus.Extensions.UnitedKingdom;
using Vehicle = Garage3.Models.Entities.Vehicle;
using System.Collections.Generic;

namespace Garage3.NewFolder
{
    public class SeedData
    {

        static UniqueRandomRegNumberGenerator generator = new UniqueRandomRegNumberGenerator();

        private static Faker faker;

            public static async Task InitAsync(GarageMVCContext context)
            {
                if (await context.Customers.AnyAsync()) return;

                faker = new Faker("sv");

                var customers = GenerateCustomers(1000);
               var (vehicles,vtypes) = GenerateVehicles(customers);

                await context.AddRangeAsync(customers);
            await context.AddRangeAsync(vtypes);
            await context.AddRangeAsync(vehicles);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Customer> GenerateCustomers(int numberOfCustomers)
        {
            var cs = new List<Customer>();

            for (int i = 0; i < numberOfCustomers; i++)
            {
                var ps = new Person();

                var fName = ps.FirstName;
                var lName = ps.LastName;
                var psn = ps.Personnummer();

                var c = new Customer
                {
                    FirstName = fName,
                    LastName = lName,
                    PersonalNumber = psn
                };
                cs.Add(c);
            }

            return cs;
        }

        private static (IEnumerable<Vehicle>,IEnumerable<VehicleType>) GenerateVehicles(IEnumerable<Customer> customers)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            var hashSet = new HashSet<string>();

            foreach (var customer in customers)
            {
                var vfactory = new Bogus.DataSets.Vehicle();
                string uniqueRegNumber = generator.GenerateUniqueRegNumber();
                var modelTypeName = vfactory.Model();

                hashSet.Add(modelTypeName);

                var newV = new Vehicle
                {
                    RegistrationNumber = uniqueRegNumber.ToString(),
                    Make = vfactory.Manufacturer(),
                    PersonalNumber = customer.PersonalNumber,
                    NumberOfWheels = GetRandomNumber(2, 4),
                    TypeName = modelTypeName,
                    Color = "White",
                };

                vehicles.Add(newV);
            }

            var vtypes = new List<VehicleType>();
            foreach (var item in hashSet)
            {
                vtypes.Add(
                    new VehicleType
                    {
                        Name = item
                    });
            }

            return (vehicles, vtypes);
        }

        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }
    }


}


