using Garage3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage3.Models.Persistence
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.PersonalNumber);
            builder.HasMany(c => c.Vehicles)
             .WithOne(v => v.Member);
             
        }
    }
}
