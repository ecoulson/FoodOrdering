using Ordering.OrderParser;

namespace Ordering.Order
{
    public interface IOrderItem
    {
        string MenuItemId();
        int Quantity();
        int Cost();
    }
}
