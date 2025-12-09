using PaymentService.Application.Events;
using PaymentService.Application.Interfaces;
using PaymentService.Application.Services.Interfaces;

namespace PaymentService.Application.Services;

public class MockPaymentProcessor : IPaymentProcessor
{
    private readonly IPaymentSucceededProducer _producer;

    public MockPaymentProcessor(IPaymentSucceededProducer producer)
    {
        _producer = producer;
    }

    public async Task ProcessAsync(PaymentRequestEvent request)
    {
        await Task.Delay(30000);

        _producer.Publish(new PaymentSucceededEvent(request.OrderId));
    }
}