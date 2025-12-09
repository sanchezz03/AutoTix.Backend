using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class TripSegmentSnapshotConfiguration : IEntityTypeConfiguration<TripSegmentSnapshot>
{
    public void Configure(EntityTypeBuilder<TripSegmentSnapshot> builder)
    {
        builder.ToTable("TripSegmentSnapshots");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SegmentId).IsRequired();
        builder.Property(s => s.DepartAt).IsRequired();
        builder.Property(s => s.ArriveAt).IsRequired();
        builder.Property(s => s.StationFrom).HasMaxLength(200);
        builder.Property(s => s.StationTo).HasMaxLength(200);

        builder.HasOne(s => s.Train)
            .WithOne()
            .HasForeignKey<TrainSnapshot>("TripSegmentSnapshotId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
