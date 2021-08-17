namespace Ordering
{
    internal class Total: ITotal
    {
        private int cost;

        public int Cost { get { return cost; } }

        public Total(int cost)
        {
            Assert.NotNegative(cost, "[Total] Cost can not be negative. Was " + cost);
            this.cost = cost;
        }

        public static Total Zero()
        {
            return new Total(0);
        }

        public void AddToTotal(IOrderItem item)
        {
            cost += item.MenuItem.Cost * item.Quantity.Value;
        }
    }
}
