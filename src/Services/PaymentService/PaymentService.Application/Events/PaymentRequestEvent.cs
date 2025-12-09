namespace PaymentService.Application.Events;

public record PaymentRequestEvent(Guid OrderId, decimal Amount);