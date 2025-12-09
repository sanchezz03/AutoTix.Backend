using OrderService.Domain.Base.Interfaces;

namespace OrderService.Domain.Events;

public class OrderPaymentFailedEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; }

    public OrderPaymentFailedEvent(Guid orderId, Guid userId)
    {
        OrderId = orderId;
        UserId = userId;
    }
}
