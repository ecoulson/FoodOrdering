using System.Collections.Generic;

namespace Ordering.Order
{
    using System;
    using Common.Id;
    using Ordering.Invoice;

    internal class Order: IOrder
    {
        private IOrderId id;
        private readonly List<IOrderItem> orderItems;

        public OrderState State { get; }
        public IId Id {
            get => id;
            set {
                if (value is IOrderId)
                {
                    id = (IOrderId)value;
                }
                else
                {
                    throw new Exception("");
                }
            }
        }

        public Order(OrderState state, List<IOrderItem> orderItems): this(
            new OrderId(),
            state,
            orderItems
        ) { }

        public Order(IOrderId id, OrderState state, List<IOrderItem> orderItems)
        {
            Assert.NotNull(id, "[Order] id must not be null");
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
            return orderItems.ConvertAll(GetInvoiceItemFromOrderItem);
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
            orderItems.ForEach(total.AddToTotal);
            return total;
        }
    }
}
