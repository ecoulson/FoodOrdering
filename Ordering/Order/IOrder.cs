using Common.Id;
using Ordering.Invoice;
using Payments;

namespace Ordering.Order
{
    public interface IOrder
    {
        OrderState State { get; }
        IId Id { get; set; }
        IInvoice GetInvoice();
    }
}
