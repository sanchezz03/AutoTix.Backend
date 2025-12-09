using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.Queries.Handlers;

public class GetUserOrdersHandler : IRequestHandler<GetUserOrdersQuery, List<Order>>
{
    private readonly IOrderRepository _repository;

    public GetUserOrdersHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Order>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetUserOrdersAsync(request.UserId);
    }
}