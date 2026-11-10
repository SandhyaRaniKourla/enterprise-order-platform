using Microsoft.EntityFrameworkCore;
using Order.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;

        public OrderRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task AddAsync(Domain.Entities.Order order, CancellationToken cancellationToken = default)
        {
            await _orderDbContext.Orders.AddAsync(order, cancellationToken);
        }

        public async Task<Domain.Entities.Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _orderDbContext.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == orderId);

        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
