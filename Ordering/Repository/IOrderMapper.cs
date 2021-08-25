using System;
using Ordering.Order;

namespace Ordering.Repository
{
    public interface IOrderMapper
    {
        IOrder ToEntity(IOrderModel model);
        IOrderModel ToModel(IOrder order);
    }
}
