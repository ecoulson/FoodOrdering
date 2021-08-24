using System;

namespace Ordering.Order
{
    internal class OrderId: IOrderId, IEquatable<OrderId>
    {
        private readonly Guid value;

        public OrderId()
        {
            value = Guid.NewGuid();
        }

        public OrderId(string id)
        {
            Assert.NotNull(id, "[OrderId] Can not create a copy of a null string");
            value = new Guid(id);
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
