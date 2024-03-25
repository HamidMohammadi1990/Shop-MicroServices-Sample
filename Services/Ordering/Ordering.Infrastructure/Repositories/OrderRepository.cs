using Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderDbContext orderContext)
    : BaseRepository<Order>(orderContext), IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        return await _context.Orders.Where(x => x.UserName == userName).ToListAsync();
    }
}