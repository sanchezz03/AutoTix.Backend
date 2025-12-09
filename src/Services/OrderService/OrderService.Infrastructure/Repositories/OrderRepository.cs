using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order> GetByIdAsync(Guid orderId)
    {
        return await _context.Orders
            .Include(o => o.Reservations)
                .ThenInclude(r => r.Trip)
                    .ThenInclude(t => t.Segments)
                        .ThenInclude(s => s.Train)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<List<Order>> GetUserOrdersAsync(Guid userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Reservations)
                .ThenInclude(r => r.Trip)
                    .ThenInclude(t => t.Segments)
                        .ThenInclude(s => s.Train)
            .ToListAsync();
    }
}
