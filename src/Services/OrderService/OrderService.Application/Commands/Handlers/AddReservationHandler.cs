using MediatR;
using OrderService.Application.Interfaces;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.Commands.Handlers;

public class AddReservationHandler : IRequestHandler<AddReservationCommand, Guid>
{
    private readonly IOrderRepository _repository;

    public AddReservationHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddReservationCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.UserId);
        var reservation = new TicketReservation(request.Quantity, request.Trip);
        order.AddReservation(reservation);

        await _repository.AddAsync(order);
        return order.Id;
    }
}