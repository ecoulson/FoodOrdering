using System;
namespace Ordering
{
    public class Total
    {
        private int cost;

        public int Cost { get { return cost; } }

        public Total(int cost)
        {
            this.cost = cost;
        }

        public static Total Zero()
        {
            return new Total(0);
        }

        public void AddToTotal(OrderItem item)
        {
            cost += item.MenuItem.Cost * item.Quantity.Value;
        }
    }
}
