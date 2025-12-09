namespace PaymentService.Application.Events;

public record PaymentSucceededEvent(Guid OrderId);
