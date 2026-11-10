using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Abstractions.Persistence
{
    public interface IOrderRepository
    {
        Task AddAsync(Order.Domain.Entities.Order order, CancellationToken cancellationToken = default);
        Task<Order.Domain.Entities.Order> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
