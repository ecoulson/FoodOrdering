﻿using System;
using Menu;

namespace Ordering
{
    public class OrderItem
    {

        public MenuItem MenuItem { get; }
        public int Quantity { get; }

        public OrderItem(MenuItem menuItem, int quantity)
        {
            MenuItem = menuItem;
            Quantity = quantity;
        }
    }
}
