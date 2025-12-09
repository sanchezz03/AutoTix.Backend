namespace OrderService.Application.DTOs;

public class PaymentRequestEvent
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
}
