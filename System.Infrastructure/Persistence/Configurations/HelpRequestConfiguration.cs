using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class HelpRequestConfiguration : IEntityTypeConfiguration<HelpRequest>
    {
        public void Configure(EntityTypeBuilder<HelpRequest> builder)
        {
            builder.Property(h => h.RequestType)
                .IsRequired();

            builder.Property(h => h.Details)
                .IsRequired();

         
        }
    }
}