using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task<Order> GetByIdAsync(Guid orderId);
    Task<List<Order>> GetUserOrdersAsync(Guid userId);
}
