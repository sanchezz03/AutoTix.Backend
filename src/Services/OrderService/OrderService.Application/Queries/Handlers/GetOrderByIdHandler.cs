using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.Queries.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly IOrderRepository _repository;
    public GetOrderByIdHandler(IOrderRepository repository) => _repository = repository;

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.OrderId);
    }
}