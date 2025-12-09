using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.DTOs;
using OrderService.Application.Queries;

namespace OrderService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-to-cart")]
    public async Task<IActionResult> AddToCart([FromBody] AddReservationRequest request)
    {
        var command = new AddReservationCommand(request.UserId, request.Trip, request.Quantity);
        var orderId = await _mediator.Send(command);
        return Ok(new { OrderId = orderId });
    }

    [HttpPost("{orderId}/pay")]
    public async Task<IActionResult> StartPayment(Guid orderId)
    {
        var command = new StartPaymentCommand(orderId);
        await _mediator.Send(command);
        return Accepted(new { Message = "Payment started" });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(Guid orderId)
    {
        var query = new GetOrderByIdQuery(orderId);
        var order = await _mediator.Send(query);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(Guid userId)
    {
        var query = new GetUserOrdersQuery(userId);
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }
}
