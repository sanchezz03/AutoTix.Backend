using PaymentService.Application.Events;

namespace PaymentService.Application.Services.Interfaces;

public interface IPaymentProcessor
{
    Task ProcessAsync(PaymentRequestEvent request);
}
