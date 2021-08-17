using System;
using Menu;

namespace Ordering
{
    public class OrderItem
    {

        public MenuItem MenuItem { get; }
        public Quantity Quantity { get; }

        public OrderItem(MenuItem menuItem, Quantity quantity)
        {
            Assert.NotNull(menuItem, "[OrderItem] Menu item can not be null");
            MenuItem = menuItem;
            Assert.NotNull(quantity, "[OrderItem] Quantity can not be null");
            Quantity = quantity;
        }
    }
}
