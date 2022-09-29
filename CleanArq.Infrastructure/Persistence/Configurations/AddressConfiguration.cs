using CleanArq.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArq.Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.Street).HasMaxLength(256);
        builder.Property(x => x.City).HasMaxLength(256);
        builder.Property(x => x.Country).HasMaxLength(256);
    }
}
