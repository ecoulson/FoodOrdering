using System;
using Ordering.Order;
using Ordering.OrderParser;

namespace Ordering.Service
{
    public class OrderService: IOrderService
    {
        private readonly IOrderParser orderParser;

        public OrderService(IOrderParser orderParser)
        {
            this.orderParser = orderParser;
        }

        public IOrder CreateOrder(ICreateOrderDto createOrderDto)
        {
            // parse order
            // save order
            throw new NotImplementedException();
        }

        public void DeleteOrder(IDeleteOrderDto deleteOrderDto)
        {
            // load order
            // delete order
            throw new NotImplementedException();
        }

        public IOrder GetOrder(IGetOrderDto getOrderDto)
        {
            // load order
            throw new NotImplementedException();
        }
    }
}
