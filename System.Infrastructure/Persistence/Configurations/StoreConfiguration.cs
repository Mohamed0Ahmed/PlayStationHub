using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired();

            builder.HasMany(s => s.Branches)
                .WithOne(b => b.Store)
                .HasForeignKey(b => b.StoreId)
                .OnDelete(DeleteBehavior.Restrict);



  

            builder.HasIndex(s => s.Name).IsUnique();
        }
    }
}