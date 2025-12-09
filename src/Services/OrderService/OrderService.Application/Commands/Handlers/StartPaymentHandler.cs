using MediatR;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;

namespace OrderService.Application.Commands.Handlers;

public class StartPaymentHandler : IRequestHandler<StartPaymentCommand, Unit>
{
    private readonly IOrderRepository _repository;
    private readonly IMessageProducer _producer;

    public StartPaymentHandler(IOrderRepository repository, IMessageProducer producer)
    {
        _repository = repository;
        _producer = producer;
    }

    public async Task<Unit> Handle(StartPaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.OrderId);
        if (order == null) throw new Exception("Order not found");

        order.MarkProcessing();
        await _repository.UpdateAsync(order);

        var paymentRequest = new PaymentRequestEvent
        {
            OrderId = order.Id,
            Amount = order.TotalAmount
        };

        _producer.Publish(paymentRequest, routingKey: "payment.request");

        return Unit.Value;
    }
}
