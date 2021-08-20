using System;
using Ordering.Order;
using Ordering.OrderParser;

namespace Ordering.Service
{
    public class OrderService: IOrderService
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
            return order;
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
