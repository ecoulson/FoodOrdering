using Ordering.Invoice;
using Payments;

namespace Ordering.Order
{
    public interface IOrder
    {
        OrderState State { get; }
        IPaymentMethod PaymentMethod { get; }
        IInvoice GetInvoice();
    }
}
