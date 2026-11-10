using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(Guid CustomerId, IReadOnlyCollection<CreateOrderItem> Items, string Currency);


    public sealed record CreateOrderItem(Guid ProductId, int Quantity, decimal UnitPrice);
}
