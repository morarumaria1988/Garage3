using Garage3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage3.Models.Persistence
{
    public class VehicleConfigurations : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(c => c.RegistrationNumber);
           // todo builder.HasAlternateKey(c => c.RegistrationNumber);
        }
    }
}
