using System;
using Ordering.Order;
using Ordering.OrderParser;

namespace Ordering.Service
{
    internal class OrderService: IOrderService
    {
        private IOrderParser orderParser;

        public OrderService(IOrderParser orderParser)
        {
            this.orderParser = orderParser;
        }

        public IOrder CreateOrder(ICreateOrderDto createOrderDto)
        {
            IOrder order = orderParser.Parse(createOrderDto.Content);
            // save order
            // request payment for order
            return order;
        }

        public void DeleteOrder(IDeleteOrderDto deleteOrderDto)
        {
            // load order
            // cancel or refund payment
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
