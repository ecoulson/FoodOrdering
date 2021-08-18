using Menu;
using Ordering.Order;

namespace Ordering.Invoice
{
    public interface IInvoiceItem
    {
        IOrderItem OrderItem { get; }
        ITotal Total { get; }
    }
}
