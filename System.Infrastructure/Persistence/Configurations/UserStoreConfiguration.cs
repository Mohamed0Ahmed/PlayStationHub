using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class UserStoreConfiguration : IEntityTypeConfiguration<UserStore>
    {
        public void Configure(EntityTypeBuilder<UserStore> builder)
        {
            builder.HasKey(us => us.Id);

            builder.HasOne(us => us.User)
                   .WithMany()
                   .HasForeignKey(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.Store)
                   .WithMany(s => s.UserStores)
                   .HasForeignKey(us => us.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}