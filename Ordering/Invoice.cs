using System;
using System.Collections.Generic;

namespace Ordering
{
    public class Invoice
    {
        private OrderId orderId;
        private List<OrderItem> orderItems;
        private Total total;

        public OrderId OrderId { get { return orderId; } }
        public List<OrderItem> Items { get { return orderItems; } }
        public Total Total { get { return total; } }

        public Invoice(OrderId orderId, List<OrderItem> orderItems, Total total)
        {
            this.orderId = orderId;
            this.orderItems = orderItems;
            this.total = total;
        }
    }
}
