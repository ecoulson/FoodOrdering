using System;

namespace Ordering.Order
{
    internal class OrderId: IOrderId, IEquatable<OrderId>
    {
        private readonly Guid value;

        public OrderId()
        {
            this.value = Guid.NewGuid();
        }

        public OrderId(OrderId copy) {
            Assert.NotNull(copy, "[OrderId] Can not create a copy of a null order id");
            value = copy.value;
        }

        public bool Equals(OrderId other)
        {
            if (other == null)
            {
                return false;
            }
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
