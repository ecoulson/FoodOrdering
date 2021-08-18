namespace Ordering.Order
{
    internal class Quantity: IQuantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            Assert.Positive(value, "[OrderItem] Quantity must have a value greater than 0. Was " + value);
            Value = value;
        }
    }
}
