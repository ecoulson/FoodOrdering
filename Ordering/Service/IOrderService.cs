using System;
using Ordering.Order;

namespace Ordering.Service
{
    public interface IOrderService
    {
        IOrder CreateOrder(ICreateOrderDto createOrderDto);
        IOrder GetOrder(IGetOrderDto getOrderDto);
        void DeleteOrder(IDeleteOrderDto deleteOrderDto);
    }
}
