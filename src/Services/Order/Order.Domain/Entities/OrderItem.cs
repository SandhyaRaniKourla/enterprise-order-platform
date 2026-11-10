using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }

        private OrderItem()
        {
            // Required by EF
        }

        public OrderItem(Guid productId, decimal unitPrice, int quantity)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentException("ProductId is required", nameof(productId));
            }
            if (unitPrice < 0)
            {
                throw new ArgumentException("UnitPrice cannot be negative", nameof(unitPrice));
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            }
            Id = Guid.NewGuid();
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public decimal GetTotal() => Quantity * UnitPrice;

    }
}
