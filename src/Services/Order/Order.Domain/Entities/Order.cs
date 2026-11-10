using Order.Domain.Enums;
using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Entities
{
    public class Order
    {
        private readonly List<OrderItem> _orderItems = [];
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public Money Total { get; private set; } = null!;

        public OrderStatus Status { get; private set; }

        public DateTimeOffset OrderAt { get; private set; }

        private Order()
        {
            // Required by EF
        }

        public Order(Guid customerId, IEnumerable<OrderItem> items, string currency)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("CustomerId cannot be empty", nameof(customerId));
            }
            var orderItems = items.ToList();
            if (orderItems == null || orderItems.Count == 0)
            {
                throw new ArgumentException("Order must have at least one item", nameof(orderItems));
            }
            //if (totalAmount == null)
            //{
            //    throw new ArgumentNullException(nameof(totalAmount), "TotalAmount is required");
            //}


            Id = Guid.NewGuid();
            CustomerId = customerId;
            _orderItems.AddRange(orderItems);
            Status = OrderStatus.Pending;
            OrderAt = DateTimeOffset.UtcNow;

            var total = _orderItems.Sum(item => item.GetTotal());
            Total = new Money(total, currency);

        }

        public void MarkInventoryReserved()
        {
            if(Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException($"Cannot reserve the inventory when status is {Status}");
            }
            Status = OrderStatus.InventoryReserved;
        }

        public void MakePaymentAuthorized()
        {
            if(Status != OrderStatus.InventoryReserved)
            {
                throw new InvalidOperationException($"Cannot Authorize the payment when status is  {Status}");
            }

            Status = OrderStatus.PaymentAuthorized;
        }

        public void ConfirmOrder()
        {
            if(Status != OrderStatus.PaymentAuthorized)
            {
                throw new InvalidOperationException($"The order can't be confirmed when the status is {Status}");
            }

            Status = OrderStatus.Confirmed;
        }

        public void CancelOrder()
        {
            if(Status == OrderStatus.Confirmed)
            {
                throw new InvalidOperationException($"The confirmed order can't be cancelled by this action");
            }

            Status = OrderStatus.Cancelled;
        }

    }
}
