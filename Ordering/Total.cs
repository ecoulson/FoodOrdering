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

        public void AddToTotal(int amount)
        {
            cost += amount;
        }
    }
}
