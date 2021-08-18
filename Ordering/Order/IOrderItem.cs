using Menu;

namespace Ordering
{
    public interface IOrderItem
    {
        IMenuItem MenuItem { get; }
        IQuantity Quantity { get; }
    }
}
