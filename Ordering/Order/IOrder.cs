using System;
using Ordering.Invoice;

namespace Ordering.Order
{
    public interface IOrder
    {
        OrderState State { get; }
        IInvoice GetInvoice();
    }
}
