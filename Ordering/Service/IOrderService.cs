using System;
using Ordering.Order;

namespace Ordering.Service
{
    public interface IOrderService
    {
        IOrder CreateOrder(ICreateOrderDto createOrderDto);
        IOrder GetOrder(IGetOrderDto getOrderDto);
        IOrder EditOrder(IUpdateOrderDto updateOrderDto);
        void DeleteOrder(IDeleteOrderDto deleteOrderDto);
    }
}
