using System.Collections.Generic;

namespace Ordering
{
    internal class Order: IOrder
    {
        private IOrderId id;
        private readonly List<IOrderItem> items;

        public OrderState State { get; }

        public Order(IOrderId id, OrderState state, List<IOrderItem> items)
        {
            Assert.NotNull(id, "[Order] ids can not be null");
            this.id = id;
            State = state;
            Assert.NotNull(items, "[Order] items list can not be null");
            this.items = new List<IOrderItem>(items);
        }

        public IInvoice getInvoice()
        {
            var total = Total.Zero();
            foreach (var orderItem in items)
            {
                total.AddToTotal(orderItem);
            }
            return new Invoice(id, new List<IOrderItem>(items), total);
        }
    }
}
