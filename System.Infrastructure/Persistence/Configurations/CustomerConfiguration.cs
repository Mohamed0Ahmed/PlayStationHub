﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.PhoneNumber)
                .IsRequired();

            builder.HasOne(c => c.Branch)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(c => c.CustomerPoints)
                .WithOne(cp => cp.Customer)
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.BranchId, c.PhoneNumber }).IsUnique();
        }
    }
}