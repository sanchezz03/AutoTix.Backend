using MediatR;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.Commands;

public record AddReservationCommand(Guid UserId, TripSnapshot Trip, int Quantity) : IRequest<Guid>;