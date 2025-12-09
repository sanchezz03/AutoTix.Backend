using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Infrastructure.Persistence;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<TicketReservation> TicketReservations { get; set; }
    public DbSet<TripSnapshot> TripSnapshots { get; set; }
    public DbSet<TripSegmentSnapshot> TripSegmentSnapshots { get; set; }
    public DbSet<TrainSnapshot> TrainSnapshots { get; set; }

    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}
