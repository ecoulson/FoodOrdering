using System;
namespace Ordering
{
    public interface IOrderService
    {
        IOrder CreateOrder(ICreateOrderDto createOrderDto);
        IOrder GetOrder(IGetOrderDto getOrderDto);
        void DeleteOrder(IDeleteOrderDto deleteOrderDto);
    }
}
