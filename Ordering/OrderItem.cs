using System;
using Menu;

namespace Ordering
{
    public class OrderItem
    {

        public MenuItem MenuItem { get; }
        public int Quantity { get; }

        public OrderItem(MenuItem menuItem, int quantity)
        {
            Assert.NotNull(menuItem, "[OrderItem] Menu item can not be null");
            MenuItem = menuItem;
            Assert.Positive(quantity, "[OrderItem] Quantity must be greater than 0. Was " + quantity);
            Quantity = quantity;
        }
    }
}
