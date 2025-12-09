using MediatR;
using OrderService.Application.Interfaces;

namespace OrderService.Application.Events;

public record PaymentFailedEvent(Guid OrderId) : INotification;

public class PaymentFailedEventHandler : INotificationHandler<PaymentFailedEvent>
{
    private readonly IOrderRepository _repository;

    public PaymentFailedEventHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(PaymentFailedEvent notification, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(notification.OrderId);
        if (order == null) return;

        order.MarkFailed();
        await _repository.UpdateAsync(order);
    }
}
