using System;
using System.Collections.Generic;

namespace Ordering
{
    public interface IInvoice
    {
        IOrderId OrderId { get; }
        List<IInvoiceItem> Items { get; }
        ITotal Total { get; }
    }
}
