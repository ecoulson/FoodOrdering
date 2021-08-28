using System;
using Common.Id;

namespace Ordering.Order
{
    internal class OrderId: Id, IOrderId
    {
        public OrderId(): base(Guid.NewGuid().ToString())
        {
        }

        public OrderId(string id) : base(id)
        {
            Guid.Parse(id);
        }
    }
}
