using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Configurations;

public class UzCredentialConfiguration : IEntityTypeConfiguration<UzCredential>
{
    public void Configure(EntityTypeBuilder<UzCredential> builder)
    {
        builder.ToTable("UzCredentials");

        builder.HasKey(e => e.Id);

        builder.Property(x => x.EncryptedAccessToken)
              .IsRequired();

        builder.Property(x => x.DeviceName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt);

        builder.HasOne(x => x.User)
            .WithMany(u => u.UzCredentials)            
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
