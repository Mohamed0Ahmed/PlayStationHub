using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
    {
        public void Configure(EntityTypeBuilder<UserBranch> builder)
        {
            builder.HasKey(ub => ub.Id);
            builder.HasOne(ub => ub.User)
                   .WithMany()
                   .HasForeignKey(ub => ub.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(ub => ub.Branch)
                   .WithMany(b => b.UserBranches)
                   .HasForeignKey(ub => ub.BranchId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}