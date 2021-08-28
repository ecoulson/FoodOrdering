using Ordering.OrderParser;

namespace Ordering.Order
{
    public interface IOrderItem
    {
        string MenuItemId { get; }
        int Quantity { get; }
        int Cost { get; }
    }
}
