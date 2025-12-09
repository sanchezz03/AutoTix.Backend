using OrderService.Domain.Base.Interfaces;

namespace OrderService.Domain.Events;

public class OrderPaidEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; }

    public OrderPaidEvent(Guid orderId, Guid userId)
    {
        OrderId = orderId;
        UserId = userId;
    }
}