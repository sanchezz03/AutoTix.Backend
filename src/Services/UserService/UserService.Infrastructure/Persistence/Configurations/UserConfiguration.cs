using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.CreatedAt);

        builder.HasMany(u => u.UzCredentials)
             .WithOne(c => c.User)
             .HasForeignKey(c => c.UserId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
