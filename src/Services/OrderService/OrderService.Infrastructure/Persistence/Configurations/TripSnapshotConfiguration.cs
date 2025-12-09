using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class TripSnapshotConfiguration : IEntityTypeConfiguration<TripSnapshot>
{
    public void Configure(EntityTypeBuilder<TripSnapshot> builder)
    {
        builder.ToTable("TripSnapshots");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.StationFrom)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.StationTo)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(t => t.Segments)
            .WithOne()
            .HasForeignKey("TripSnapshotId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}