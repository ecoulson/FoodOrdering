using System;
namespace Ordering
{
    public interface IOrder
    {
        OrderState State { get; }
        IInvoice GetInvoice();
    }
}
