using Menu;

namespace Ordering
{
    internal class OrderItem: IOrderItem
    {
        public IMenuItem MenuItem { get; }
        public IQuantity Quantity { get; }

        public OrderItem(IMenuItem menuItem, IQuantity quantity)
        {
            Assert.NotNull(menuItem, "[OrderItem] Menu item can not be null");
            MenuItem = menuItem;
            Assert.NotNull(quantity, "[OrderItem] Quantity can not be null");
            Quantity = quantity;
        }
    }
}
