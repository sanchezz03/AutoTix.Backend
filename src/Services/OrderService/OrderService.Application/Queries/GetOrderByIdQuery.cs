using MediatR;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.Queries;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<Order>;
