using System;
using Menu;
using Ordering.OrderParser;

namespace Ordering.Order
{
    internal class OrderItem: IOrderItem, IEquatable<IOrderItem>
    {
        private readonly IQuantity quantity;
        private readonly ICost cost;
        private readonly IMenuItemIdentifier menuItemIdentifier;

        public OrderItem(IMenuItemIdentifier menuItemIdentifier, IQuantity quantity, ICost cost)
        {
            Assert.NotNull(menuItemIdentifier, "[OrderItem] Menu item identifier can not be null");
            this.menuItemIdentifier = menuItemIdentifier;
            Assert.NotNull(quantity, "[OrderItem] Quantity can not be null");
            this.quantity = quantity;
            Assert.NotNull(cost, "[OrderItem] Cost can not be null");
            this.cost = cost;
        }

        public OrderItem(IMenuItemIdentifier menuItemId, IQuantity quantity):
            this(menuItemId, quantity, new Cost(0)) { }

        public int Cost()
        {
            return cost.Value * quantity.Value;
        }

        public string MenuItemId()
        {
            return menuItemIdentifier.Value;
        }

        public int Quantity()
        {
            return quantity.Value;
        }

        public bool Equals(IOrderItem other)
        {
            return other.MenuItemId() == MenuItemId() &&
                other.Quantity() == Quantity();
        }
    }
}
