using MediatR;

namespace OrderService.Application.Commands;

public record StartPaymentCommand(Guid OrderId) : IRequest<Unit>;
