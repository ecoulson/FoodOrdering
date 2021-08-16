using System.Collections.Generic;

namespace Ordering
{
    public class Order
    {
        private OrderId id;
        private List<OrderItem> items;

        public OrderState State { get; }

        public Order(OrderId id, OrderState state, List<OrderItem> items)
        {
            Assert.NotNull(id, "Order ids can not be null");
            this.id = id;
            State = state;
            Assert.NotNull(items, "Order items list can not be null");
            this.items = new List<OrderItem>(items);
        }

        public Invoice getInvoice()
        {
            var total = new Total(0);
            foreach (var orderItem in items)
            {
                total.AddToTotal(orderItem.MenuItem.Cost);
            }
            return new Invoice(id, new List<OrderItem>(items), total);
        }
    }
}
