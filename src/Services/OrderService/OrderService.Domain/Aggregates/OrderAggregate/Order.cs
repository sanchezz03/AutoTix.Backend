using OrderService.Domain.Base;
using OrderService.Domain.Enums;
using OrderService.Domain.Events;

namespace OrderService.Domain.Aggregates.OrderAggregate;

public class Order : AggregateRoot
{
    private readonly List<TicketReservation> _reservations = new();

    public Guid UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }

    public IReadOnlyCollection<TicketReservation> Reservations => _reservations;

    private Order() { } 

    public Order(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Status = OrderStatus.New;
    }

    public void AddReservation(TicketReservation reservation)
    {
        _reservations.Add(reservation);
        RecalculateAmount();
    }

    public void MarkProcessing()
    {
        Status = OrderStatus.Processing;
        AddDomainEvent(new OrderPaymentStartedEvent(Id, UserId, TotalAmount));
    }

    public void MarkPaid()
    {
        Status = OrderStatus.Paid;
        AddDomainEvent(new OrderPaidEvent(Id, UserId));
    }

    public void MarkFailed()
    {
        Status = OrderStatus.Failed;
        AddDomainEvent(new OrderPaymentFailedEvent(Id, UserId));
    }

    private void RecalculateAmount()
    {
        TotalAmount = _reservations.Sum(r => r.CalculateAmount());
    }
}
