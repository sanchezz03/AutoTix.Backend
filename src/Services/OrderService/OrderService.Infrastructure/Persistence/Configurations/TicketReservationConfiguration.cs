using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class TicketReservationConfiguration : IEntityTypeConfiguration<TicketReservation>
{
    public void Configure(EntityTypeBuilder<TicketReservation> builder)
    {
        builder.ToTable("TicketReservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Quantity)
            .IsRequired();

        builder.HasOne(r => r.Trip)
            .WithOne()
            .HasForeignKey<TripSnapshot>("TicketReservationId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
