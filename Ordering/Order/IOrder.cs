using Ordering.Invoice;

namespace Ordering.Order
{
    public interface IOrder
    {
        OrderState State { get; }
        IOrderId Id { get; set; }
        IInvoice GetInvoice();
    }
}
