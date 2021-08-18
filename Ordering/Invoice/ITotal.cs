using Ordering.Order;

namespace Ordering.Invoice
{
    public interface ITotal
    {
        int Cost { get; }
        void AddToTotal(IOrderItem item);
    }
}
