using MediatR;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.Queries;

public record GetUserOrdersQuery(Guid UserId) : IRequest<List<Order>>;
