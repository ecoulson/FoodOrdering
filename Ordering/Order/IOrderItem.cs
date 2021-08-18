using Menu;

namespace Ordering.Order
{
    public interface IOrderItem
    {
        IMenuItem MenuItem { get; }
        IQuantity Quantity { get; }
    }
}
