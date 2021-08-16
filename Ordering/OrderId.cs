using System;

namespace Ordering
{
    public class OrderId: IEquatable<OrderId>
    {
        public Guid Value { get; }

        public OrderId()
        {
            Value = Guid.NewGuid();
        }

        public OrderId(OrderId copy) {
            Assert.NotNull(copy, "[OrderId] Can not create a copy of a null order id");
            Value = copy.Value;
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
