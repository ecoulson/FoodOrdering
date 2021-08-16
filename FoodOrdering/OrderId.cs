using System;

namespace Ordering
{
    public class OrderId: IEquatable<OrderId>
    {
        public Guid Value { get; }

        public OrderId(): this(Guid.NewGuid()) { }

        public OrderId(OrderId copy) : this(copy.Value) { }

        private OrderId(Guid value)
        {
            Value = value;
        }

        public bool Equals(OrderId other)
        {
            if (other == null)
            {
                return false;
            }
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
