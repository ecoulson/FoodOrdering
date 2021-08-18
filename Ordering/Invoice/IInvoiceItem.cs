using Menu;

namespace Ordering
{
    public interface IInvoiceItem
    {
        IOrderItem OrderItem { get; }
        ITotal Total { get; }
    }
}
