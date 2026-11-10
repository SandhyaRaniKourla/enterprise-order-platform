using Order.Application.Abstractions.Persistence;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderResult> HandleAsync(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
        {
            var items = createOrderCommand.Items.Select(item => new OrderItem(item.ProductId, item.UnitPrice, item.Quantity)).ToList();

            var order = new Order.Domain.Entities.Order(createOrderCommand.CustomerId, items, createOrderCommand.Currency);
            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id, order.Status.ToString(), order.Total.Amount, order.Total.Currency);
        }

        public sealed record CreateOrderResult(Guid OrderId, string OrderStatus, decimal OrderTotalAmount, string Currency);
    }
}
