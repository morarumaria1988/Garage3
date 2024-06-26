﻿using Garage3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Garage3.Models.Persistence
{
    public class VehicleConfigurations : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(c => c.RegistrationNumber);
            builder.HasOne(v => v.Member)
           .WithMany(c => c.Vehicles)
           .HasForeignKey(v => v.PersonalNumber);
            builder.HasMany(v => v.Receipts)
                .WithOne(r => r.Fordon)
                .HasForeignKey(r => r.RegistrationNumber);

            builder.HasOne(v => v.VType)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(v => v.TypeName);
          

        }
    }
}
