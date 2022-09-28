using CleanArq.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArq.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(d => d.FirstName)
            .HasMaxLength(256);
        builder.Property(d => d.LastName)
            .HasMaxLength(256);
        builder.Property(d => d.Email)
            .HasMaxLength(320);
    }
}
