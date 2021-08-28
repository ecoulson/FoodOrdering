using System.Collections.Generic;
using Common.Id;

namespace Ordering.Invoice
{
    public interface IInvoice
    {
        IId OrderId { get; }
        List<IInvoiceItem> Items { get; }
        ITotal Total { get; }
    }
}
