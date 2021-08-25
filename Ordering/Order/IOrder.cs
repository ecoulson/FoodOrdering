using Ordering.Invoice;
using Payments;

namespace Ordering.Order
{
    public interface IOrder
    {
        OrderState State { get; }
        void SetId(IOrderId id);
        string Id();
        IInvoice GetInvoice();
    }
}
