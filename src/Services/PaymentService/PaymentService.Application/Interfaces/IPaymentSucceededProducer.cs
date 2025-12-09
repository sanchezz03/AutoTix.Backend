using PaymentService.Application.Events;

namespace PaymentService.Application.Interfaces;

public interface IPaymentSucceededProducer
{
    void Publish(PaymentSucceededEvent evt);
}
