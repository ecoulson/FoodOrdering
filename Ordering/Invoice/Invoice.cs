using System.Collections.Generic;
using Ordering.Order;

namespace Ordering.Invoice
{
    internal class Invoice: IInvoice
    {
        public IOrderId OrderId { get; }
        public List<IInvoiceItem> Items { get; }
        public ITotal Total { get; }

        public Invoice(IOrderId orderId, List<IInvoiceItem> invoiceItems, ITotal total)
        {
            Assert.NotNull(orderId, "[Invoice] OrderId it can not be null");
            OrderId = orderId;
            Assert.NotNull(invoiceItems, "[Invoice] Invoice items can not be null");
            Items = invoiceItems;
            Assert.NotNull(total, "[Invoice] Total can not be null");
            Total = total;
        }
    }
}
