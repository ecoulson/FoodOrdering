using System;
using Ordering.Order;

namespace Ordering.Repository
{
    public class OrderFileRepository: IOrderRepository
    {
        public OrderFileRepository()
        {
        }

        public IOrder Create(IOrder order)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IOrder order)
        {
            throw new NotImplementedException();
        }

        public IOrder Read(IOrderId orderId)
        {
            throw new NotImplementedException();
        }

        public IOrder Update(IOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
