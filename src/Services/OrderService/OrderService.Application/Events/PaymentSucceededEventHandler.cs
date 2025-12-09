using MediatR;
using OrderService.Application.Interfaces;

namespace OrderService.Application.Events;

public record PaymentSucceededEvent(Guid OrderId) : INotification;

public class PaymentSucceededEventHandler : INotificationHandler<PaymentSucceededEvent>
{
    private readonly IOrderRepository _repository;

    public PaymentSucceededEventHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(PaymentSucceededEvent notification, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(notification.OrderId);
        if (order == null) return;

        order.MarkPaid();
        await _repository.UpdateAsync(order);
    }
}
