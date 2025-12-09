using OrderService.Domain.Base.Interfaces;

namespace OrderService.Domain.Events;

public class OrderPaymentStartedEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }

    public OrderPaymentStartedEvent(Guid orderId, Guid userId, decimal amount)
    {
        OrderId = orderId;
        UserId = userId;
        Amount = amount;
    }
}