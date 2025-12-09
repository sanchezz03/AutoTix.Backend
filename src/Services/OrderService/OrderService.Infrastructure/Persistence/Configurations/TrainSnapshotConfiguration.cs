using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class TrainSnapshotConfiguration : IEntityTypeConfiguration<TrainSnapshot>
{
    public void Configure(EntityTypeBuilder<TrainSnapshot> builder)
    {
        builder.ToTable("TrainSnapshots");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.TrainId).IsRequired();

        builder.Property(t => t.StationFrom).HasMaxLength(200);
        builder.Property(t => t.StationTo).HasMaxLength(200);
        builder.Property(t => t.Number).HasMaxLength(50);

        builder.Property(t => t.WagonClassesJson)
           .IsRequired()
           .HasColumnType("nvarchar(max)");

        builder.Ignore(t => t.WagonClasses);
    }
}