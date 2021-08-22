using System;
using System.Collections.Generic;
using Ordering.Order;

namespace Ordering.Repository
{
    public interface IOrderRepository
    {
        void Create(IOrder order);
        IOrder Read(IOrderId orderId);
        void Update(IOrderId id, IOrder order);
        bool Delete(IOrder order);
    }
}
