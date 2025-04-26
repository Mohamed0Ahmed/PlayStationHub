using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(r => r.RoomName)
                .IsRequired();

       

            builder.HasMany(r => r.Guests)
                .WithOne(g => g.Room)
                .HasForeignKey(g => g.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

   
        }
    }
}