using System.Collections.Generic;
using Ordering.Order;

namespace Ordering.Invoice
{
    public interface IInvoice
    {
        IOrderId OrderId { get; }
        List<IInvoiceItem> Items { get; }
        ITotal Total { get; }
    }
}
