using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.DTOs;

public class AddReservationRequest
{
    public Guid UserId { get; set; }
    public TripSnapshot Trip { get; set; } = default!;
    public int Quantity { get; set; }
}
