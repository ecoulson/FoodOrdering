﻿using Ordering.Order;

namespace Ordering.Repository
{
    public interface IOrderRepository
    {
        IOrder Create(IOrder order);
        IOrder Read(IOrderId orderId);
        IOrder Update(IOrder order);
        bool Delete(IOrder order);
    }
}
