using System;
namespace Ordering.Order
{
    public class Cost: ICost
    {
        public int Value { get; }

        public Cost(int value)
        {
            Value = value;
        }
    }
}
