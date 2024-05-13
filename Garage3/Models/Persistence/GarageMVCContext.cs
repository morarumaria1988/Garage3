using Garage3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Emit;

namespace Garage3.Models.Persistence
{
    public class GarageMVCContext : DbContext
    {
        public GarageMVCContext(DbContextOptions<GarageMVCContext> options)
            : base(options)
        {
     
        }

        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        public DbSet<Receipt> Receipts { get; set; } = default!;

        public DbSet<VehicleType> VTypes { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfigurations());
            modelBuilder.ApplyConfiguration(new VehicleConfigurations());

         }

    }
}
