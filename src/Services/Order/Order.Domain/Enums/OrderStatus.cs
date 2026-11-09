using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        InventoryReserved = 2,
        PaymentAuthorized = 3,
        Confirmed = 4,
        Cancelled = 5
    }
}
