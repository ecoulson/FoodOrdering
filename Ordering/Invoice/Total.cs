using Ordering.Order;

namespace Ordering.Invoice
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

        public static Total FromOrderItem(IOrderItem orderItem)
        {
            return new Total(orderItem.Cost);
        }

        public void AddToTotal(IOrderItem orderItem)
        {
            Assert.NotNull(orderItem, "[Total::AddToTotal] Can not add a null item to the total");

            cost += orderItem.Cost;

            Assert.Positive(cost, "[Total::AddToTotal] Should not have a negative cost after adding an order item to the total");
        }
    }
}
