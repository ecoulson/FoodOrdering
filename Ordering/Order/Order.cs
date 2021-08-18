using System.Collections.Generic;

namespace Ordering.Order
{
    using Ordering.Invoice;

    internal class Order: IOrder
    {
        private IOrderId id;
        private readonly List<IOrderItem> orderItems;

        public OrderState State { get; }

        public Order(IOrderId id, OrderState state, List<IOrderItem> orderItems)
        {
            Assert.NotNull(id, "[Order] ids can not be null");
            this.id = id;
            State = state;
            Assert.NotNull(orderItems, "[Order] items list can not be null");
            this.orderItems = new List<IOrderItem>(orderItems);
        }

        public IInvoice GetInvoice()
        {
            return new Invoice(id, GetInvoiceItems(), GetOrderTotal());
        }

        private List<IInvoiceItem> GetInvoiceItems()
        {
            return orderItems.ConvertAll((orderItem) => GetInvoiceItemFromOrderItem(orderItem));
        }

        private IInvoiceItem GetInvoiceItemFromOrderItem(IOrderItem item)
        {
            var itemTotal = Total.Zero();
            itemTotal.AddToTotal(item);
            return new InvoiceItem(item, itemTotal);
        }

        private ITotal GetOrderTotal()
        {
            var total = Total.Zero();
            orderItems.ForEach((orderItem) => total.AddToTotal(orderItem));
            return total;
        }
    }
}
