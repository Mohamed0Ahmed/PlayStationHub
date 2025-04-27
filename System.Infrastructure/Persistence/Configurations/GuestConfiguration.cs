using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(g => g.Username)
                .IsRequired();

            builder.Property(g => g.Password)
                .IsRequired();

            builder.Property(g => g.CurrentSessionId)
            .IsRequired(false);


            builder.HasOne(g => g.Room)
                .WithMany(r => r.Guests)
                .HasForeignKey(g => g.RoomId)
                .OnDelete(DeleteBehavior.Restrict);



            builder.HasIndex(g => new { g.StoreId, g.Username }).IsUnique();

            builder.HasIndex(g => g.RoomId).IsUnique();

        }
    }
}