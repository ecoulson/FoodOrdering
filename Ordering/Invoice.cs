using System;
using System.Collections.Generic;

namespace Ordering
{
    internal class Invoice: IInvoice
    {
        public IOrderId OrderId { get; }
        public List<IOrderItem> Items { get; }
        public ITotal Total { get; }

        public Invoice(IOrderId orderId, List<IOrderItem> orderItems, ITotal total)
        {
            OrderId = orderId;
            Items = orderItems;
            Total = total;
        }
    }
}
